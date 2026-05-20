using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TopinLite.Domain.Configuration;
using TopinLite.Workers.LogWorker.Configuration;

namespace TopinLite.Workers.LogWorker.Consumers;

internal abstract class RabbitConsumerBase<TMessage, TRow> : BackgroundService
    where TMessage : class
    where TRow : class
{
    private const string RetryCountHeader = "x-retry-count";
    private readonly RabbitMqConfigModel _rabbitConfig;
    private readonly RabbitMqConnectionConfigModel _connectionConfig;
    private readonly LogConsumerOptions _options;
    private readonly ILogger _logger;
 
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
 
    protected RabbitConsumerBase(
        IOptions<RabbitMqConfigModel> rabbitOptions,
        IOptions<LogConsumerOptions> consumerOptions,
        ILogger logger)
    {
        _rabbitConfig = rabbitOptions.Value;
        _connectionConfig = _rabbitConfig.RabbitConnection;
        _options = consumerOptions.Value;
        _logger = logger;
    }

    protected abstract RabbitMqQueueConfigModel QueueConfig { get; }

    /// <summary>The RabbitMQ queue this consumer reads from.</summary>
    protected string QueueName => QueueConfig.QueueName;
 
    /// <summary>Friendly name used in logs.</summary>
    protected abstract string ConsumerName { get; }
 
    /// <summary>Convert a deserialized message to the DB row shape.</summary>
    protected abstract TRow Map(TMessage message);
 
    /// <summary>Bulk-insert a batch of rows into Postgres.</summary>
    protected abstract Task FlushAsync(IReadOnlyList<TRow> batch, CancellationToken cancellationToken);

    private bool IsRetryEnabled => HasRetryTopology(QueueConfig);

    private int MaxRetries
    {
        get
        {
            int configured = QueueConfig.MaxRetryCount ?? _rabbitConfig.MaxRetries;
            return configured > 0 ? configured : 5;
        }
    }
    
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await RunOneSessionAsync(stoppingToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "{Consumer}: connection/session failed, reconnecting in {Delay}ms",
                    ConsumerName,
                    _options.RabbitReconnectDelayMilliseconds);
                await SafeDelay(_options.RabbitReconnectDelayMilliseconds, stoppingToken).ConfigureAwait(false);
            }
        }
    }
 
    private async Task RunOneSessionAsync(CancellationToken stoppingToken)
    {
        ConnectionFactory factory = new()
        {
            HostName = _connectionConfig.HostAddress,
            Port = _connectionConfig.PortNumber,
            UserName = _connectionConfig.Username,
            Password = _connectionConfig.Password,
            ClientProvidedName = $"{_connectionConfig.ClientName}.{ConsumerName}",
            AutomaticRecoveryEnabled = true,
            TopologyRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };
 
        await using IConnection connection = await factory.CreateConnectionAsync(stoppingToken).ConfigureAwait(false);
        await using IChannel channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken).ConfigureAwait(false);

        await DeclareTopologyAsync(channel, stoppingToken).ConfigureAwait(false);
 
        // PrefetchCount bounds in-flight unacked messages.
        await channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: _options.PrefetchCount,
            global: false,
            cancellationToken: stoppingToken).ConfigureAwait(false);
 
        _logger.LogInformation(
            "{Consumer}: subscribed to '{Queue}' (prefetch={Prefetch}, batch={Batch})",
            ConsumerName, QueueName, _options.PrefetchCount, _options.BatchSize);
 
        var consumer = new AsyncEventingBasicConsumer(channel);
        var pending = new List<PendingDelivery<TRow>>(_options.BatchSize);
        var pendingLock = new SemaphoreSlim(1, 1);
 
        consumer.ReceivedAsync += async (_, ea) =>
        {
            // Per-message try/catch so one poison message can't kill the consumer.
            TRow row;
            try
            {
                TMessage message = Deserialize(ea.Body)
                    ?? throw new InvalidOperationException("Deserialized payload was null.");
                row = Map(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "{Consumer}: failed to parse/map message (deliveryTag={Tag})",
                    ConsumerName, ea.DeliveryTag);

                if (IsRetryEnabled)
                {
                    await PublishToDeadLetterAsync(channel, ea.Body.ToArray(), CopyHeaders(ea.BasicProperties?.Headers), GetRetryCount(ea.BasicProperties), "poison-message")
                        .ConfigureAwait(false);
                }

                try
                {
                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken)
                        .ConfigureAwait(false);
                }
                catch
                {
                    // channel may be gone — next session will reconnect
                }
                return;
            }
 
            await pendingLock.WaitAsync(stoppingToken).ConfigureAwait(false);
            try
            {
                pending.Add(new PendingDelivery<TRow>(
                    ea.DeliveryTag,
                    row,
                    ea.Body.ToArray(),
                    CopyHeaders(ea.BasicProperties?.Headers),
                    GetRetryCount(ea.BasicProperties)));
            }
            finally
            {
                pendingLock.Release();
            }
        };
 
        string consumerTag = await channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken).ConfigureAwait(false);
 
        // Flush loop — runs alongside the receive callbacks above.
        using var flushTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(
            Math.Max(100, _options.FlushIntervalMilliseconds)));
 
        while (!stoppingToken.IsCancellationRequested)
        {
            await flushTimer.WaitForNextTickAsync(stoppingToken).ConfigureAwait(false);
 
            // Snapshot pending items.
            List<PendingDelivery<TRow>> snapshot;
            await pendingLock.WaitAsync(stoppingToken).ConfigureAwait(false);
            try
            {
                if (pending.Count == 0)
                {
                    continue;
                }
 
                int take = Math.Min(pending.Count, _options.BatchSize);
                snapshot = pending.GetRange(0, take);
                pending.RemoveRange(0, take);
            }
            finally
            {
                pendingLock.Release();
            }
 
            try
            {
                List<TRow> rows = snapshot.ConvertAll(p => p.Row);
                await FlushAsync(rows, stoppingToken).ConfigureAwait(false);
 
                // Ack with multiple=true on the highest delivery tag of the batch.
                ulong lastTag = snapshot[^1].DeliveryTag;
                await channel.BasicAckAsync(lastTag, multiple: true, cancellationToken: stoppingToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "{Consumer}: Postgres flush failed for {Count} messages",
                    ConsumerName, snapshot.Count);

                if (IsRetryEnabled)
                {
                    await HandleFailedBatchWithRetryAsync(channel, snapshot).ConfigureAwait(false);
                    continue;
                }
 
                // Put the snapshot back at the FRONT of pending so order is
                // preserved when we resume. We must hold them in memory because
                // RabbitMQ has already delivered them; they stay unacked, so on
                // a process restart they'd be redelivered, but during this
                // process's lifetime we keep them ourselves.
                await pendingLock.WaitAsync(CancellationToken.None).ConfigureAwait(false);
                try
                {
                    pending.InsertRange(0, snapshot);
                }
                finally
                {
                    pendingLock.Release();
                }
 
                // Cancel the consumer so RabbitMQ stops sending us more.
                // Existing prefetched-but-unacked messages stay on the broker;
                // we tear the channel down by leaving this method, then the
                // outer loop reconnects after the configured delay.
                try
                {
                    await channel.BasicCancelAsync(consumerTag, cancellationToken: CancellationToken.None)
                        .ConfigureAwait(false);
                }
                catch
                {
                    // ignored — channel may already be unhealthy
                }
 
                await SafeDelay(_options.PostgresRetryDelayMilliseconds, stoppingToken).ConfigureAwait(false);
 
                // Bail out of this session; outer loop will reconnect with a
                // fresh channel and a new consumer tag. Any rows left in the
                // local `pending` list are dropped, but they're also still
                // unacked on the broker (the channel was never acked from
                // them on success), so RabbitMQ will redeliver them.
                return;
            }
        }
    }
 
    private static TMessage? Deserialize(ReadOnlyMemory<byte> body)
    {
        // Producer side does System.Text.Json.JsonSerializer.Serialize(model)
        // and publishes UTF-8 bytes; deserialize symmetrically.
        return JsonSerializer.Deserialize<TMessage>(body.Span, JsonOptions);
    }
 
    private static async Task SafeDelay(int milliseconds, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(milliseconds, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // expected on shutdown
        }
    }

    private async Task DeclareTopologyAsync(IChannel channel, CancellationToken cancellationToken)
    {
        if (IsRetryEnabled)
        {
            await channel.ExchangeDeclareAsync(
                exchange: QueueConfig.ExchangeName,
                type: ExchangeType.Direct,
                durable: QueueConfig.ExchangeDurable,
                autoDelete: QueueConfig.ExchangeAutoDelete,
                arguments: null,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await channel.ExchangeDeclareAsync(
                exchange: QueueConfig.RetryExchangeName,
                type: ExchangeType.Direct,
                durable: QueueConfig.ExchangeDurable,
                autoDelete: QueueConfig.ExchangeAutoDelete,
                arguments: null,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await channel.ExchangeDeclareAsync(
                exchange: QueueConfig.DeadLetterExchangeName,
                type: ExchangeType.Direct,
                durable: QueueConfig.ExchangeDurable,
                autoDelete: QueueConfig.ExchangeAutoDelete,
                arguments: null,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await channel.QueueDeclareAsync(
                queue: QueueConfig.RetryQueueName,
                durable: QueueConfig.QueueDurable,
                exclusive: false,
                autoDelete: QueueConfig.QueueAutoDelete,
                arguments: new Dictionary<string, object?>
                {
                    ["x-message-ttl"] = ResolveRetryDelayMilliseconds(),
                    ["x-dead-letter-exchange"] = QueueConfig.ExchangeName,
                    ["x-dead-letter-routing-key"] = QueueConfig.RoutingKey
                },
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await channel.QueueBindAsync(
                queue: QueueConfig.RetryQueueName,
                exchange: QueueConfig.RetryExchangeName,
                routingKey: QueueConfig.RetryRoutingKey,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await channel.QueueDeclareAsync(
                queue: QueueConfig.DeadLetterQueueName,
                durable: QueueConfig.QueueDurable,
                exclusive: false,
                autoDelete: QueueConfig.QueueAutoDelete,
                arguments: null,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await channel.QueueBindAsync(
                queue: QueueConfig.DeadLetterQueueName,
                exchange: QueueConfig.DeadLetterExchangeName,
                routingKey: QueueConfig.DeadLetterRoutingKey,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: QueueConfig.QueueDurable,
            exclusive: false,
            autoDelete: QueueConfig.QueueAutoDelete,
            arguments: IsRetryEnabled
                ? new Dictionary<string, object?>
                {
                    ["x-dead-letter-exchange"] = QueueConfig.DeadLetterExchangeName,
                    ["x-dead-letter-routing-key"] = QueueConfig.DeadLetterRoutingKey
                }
                : null,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        if (IsRetryEnabled)
        {
            await channel.QueueBindAsync(
                queue: QueueName,
                exchange: QueueConfig.ExchangeName,
                routingKey: QueueConfig.RoutingKey,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task HandleFailedBatchWithRetryAsync(IChannel channel, IReadOnlyList<PendingDelivery<TRow>> snapshot)
    {
        foreach (PendingDelivery<TRow> message in snapshot)
        {
            bool maxRetryReached = message.RetryCount >= MaxRetries;
            bool handled;

            if (maxRetryReached)
            {
                handled = await PublishToDeadLetterAsync(
                    channel,
                    message.Body,
                    message.Headers,
                    message.RetryCount,
                    "max-retries-reached").ConfigureAwait(false);
            }
            else
            {
                handled = await PublishToRetryAsync(
                    channel,
                    message.Body,
                    message.Headers,
                    message.RetryCount + 1).ConfigureAwait(false);
            }

            if (handled)
            {
                await channel.BasicAckAsync(message.DeliveryTag, multiple: false, cancellationToken: CancellationToken.None)
                    .ConfigureAwait(false);
            }
            else
            {
                await channel.BasicNackAsync(message.DeliveryTag, multiple: false, requeue: true, cancellationToken: CancellationToken.None)
                    .ConfigureAwait(false);
            }
        }
    }

    private async Task<bool> PublishToRetryAsync(
        IChannel channel,
        ReadOnlyMemory<byte> body,
        IDictionary<string, object?> headers,
        int retryCount)
    {
        try
        {
            BasicProperties properties = BuildProperties(headers, retryCount, null);
            await channel.BasicPublishAsync(
                exchange: QueueConfig.RetryExchangeName,
                routingKey: QueueConfig.RetryRoutingKey,
                mandatory: false,
                basicProperties: properties,
                body: body,
                cancellationToken: CancellationToken.None).ConfigureAwait(false);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "{Consumer}: failed to publish retry message",
                ConsumerName);
            return false;
        }
    }

    private async Task<bool> PublishToDeadLetterAsync(
        IChannel channel,
        ReadOnlyMemory<byte> body,
        IDictionary<string, object?> headers,
        int retryCount,
        string reason)
    {
        try
        {
            BasicProperties properties = BuildProperties(headers, retryCount, reason);
            await channel.BasicPublishAsync(
                exchange: QueueConfig.DeadLetterExchangeName,
                routingKey: QueueConfig.DeadLetterRoutingKey,
                mandatory: false,
                basicProperties: properties,
                body: body,
                cancellationToken: CancellationToken.None).ConfigureAwait(false);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "{Consumer}: failed to publish dead-letter message",
                ConsumerName);
            return false;
        }
    }

    private BasicProperties BuildProperties(IDictionary<string, object?> sourceHeaders, int retryCount, string? reason)
    {
        Dictionary<string, object?> headers = sourceHeaders.Count == 0
            ? []
            : new Dictionary<string, object?>(sourceHeaders);

        headers[RetryCountHeader] = retryCount;
        if (!string.IsNullOrWhiteSpace(reason))
        {
            headers["x-dead-letter-reason"] = reason;
        }

        return new BasicProperties
        {
            ContentType = "application/json",
            DeliveryMode = DeliveryModes.Persistent,
            Headers = headers
        };
    }

    private int ResolveRetryDelayMilliseconds()
    {
        int defaultDelay = _rabbitConfig.RetryDelayMilliseconds > 0 ? _rabbitConfig.RetryDelayMilliseconds : 30000;
        int delay = QueueConfig.RetryDelayMilliseconds ?? defaultDelay;
        return delay > 0 ? delay : defaultDelay;
    }

    private static int GetRetryCount(IReadOnlyBasicProperties? properties)
    {
        if (properties?.Headers == null)
        {
            return 0;
        }

        if (!properties.Headers.TryGetValue(RetryCountHeader, out object? value) || value == null)
        {
            return 0;
        }

        return value switch
        {
            byte b => b,
            sbyte sb => sb,
            short s => s,
            ushort us => us,
            int i => i,
            uint ui => (int)ui,
            long l => l > int.MaxValue ? int.MaxValue : (int)l,
            ulong ul => ul > int.MaxValue ? int.MaxValue : (int)ul,
            byte[] bytes when int.TryParse(Encoding.UTF8.GetString(bytes), out int parsed) => parsed,
            string text when int.TryParse(text, out int parsed) => parsed,
            _ => 0
        };
    }

    private static IDictionary<string, object?> CopyHeaders(IDictionary<string, object?>? headers)
    {
        return headers is null || headers.Count == 0
            ? new Dictionary<string, object?>()
            : new Dictionary<string, object?>(headers);
    }

    private static bool HasRetryTopology(RabbitMqQueueConfigModel queueConfig)
    {
        return !string.IsNullOrWhiteSpace(queueConfig.RetryExchangeName) &&
               !string.IsNullOrWhiteSpace(queueConfig.RetryQueueName) &&
               !string.IsNullOrWhiteSpace(queueConfig.RetryRoutingKey) &&
               !string.IsNullOrWhiteSpace(queueConfig.DeadLetterExchangeName) &&
               !string.IsNullOrWhiteSpace(queueConfig.DeadLetterQueueName) &&
               !string.IsNullOrWhiteSpace(queueConfig.DeadLetterRoutingKey);
    }

    private readonly record struct PendingDelivery<T>(
        ulong DeliveryTag,
        T Row,
        byte[] Body,
        IDictionary<string, object?> Headers,
        int RetryCount);

}
