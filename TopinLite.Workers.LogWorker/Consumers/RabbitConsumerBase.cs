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
        _connectionConfig = rabbitOptions.Value.RabbitConnection;
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
        RabbitMqQueueConfigModel queueConfig = QueueConfig;
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
 
        // Declare queue defensively — same flags the producer uses. Idempotent.
        Dictionary<string, object?> queueArguments = [];
        if (queueConfig.IsRetryEnabled)
        {
            queueArguments["x-dead-letter-exchange"] = queueConfig.EffectiveRetryExchangeName;
            queueArguments["x-dead-letter-routing-key"] = queueConfig.EffectiveRetryRoutingKey;
        }

        await channel.ExchangeDeclareAsync(
            exchange: queueConfig.ExchangeName,
            type: ExchangeType.Direct,
            durable: queueConfig.ExchangeDurable,
            autoDelete: queueConfig.ExchangeAutoDelete,
            arguments: null,
            cancellationToken: stoppingToken).ConfigureAwait(false);

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: queueConfig.QueueDurable,
            exclusive: false,
            autoDelete: queueConfig.QueueAutoDelete,
            arguments: queueArguments.Count == 0 ? null : queueArguments,
            cancellationToken: stoppingToken).ConfigureAwait(false);

        await channel.QueueBindAsync(
            queue: QueueName,
            exchange: queueConfig.ExchangeName,
            routingKey: queueConfig.RoutingKey,
            cancellationToken: stoppingToken).ConfigureAwait(false);

        if (queueConfig.IsRetryEnabled)
        {
            await channel.ExchangeDeclareAsync(
                exchange: queueConfig.EffectiveRetryExchangeName,
                type: ExchangeType.Direct,
                durable: queueConfig.ExchangeDurable,
                autoDelete: queueConfig.ExchangeAutoDelete,
                arguments: null,
                cancellationToken: stoppingToken).ConfigureAwait(false);

            Dictionary<string, object?> retryQueueArguments = new()
            {
                ["x-message-ttl"] = queueConfig.RetryDelayMilliseconds > 0 ? queueConfig.RetryDelayMilliseconds : 30000,
                ["x-dead-letter-exchange"] = queueConfig.ExchangeName,
                ["x-dead-letter-routing-key"] = queueConfig.RoutingKey
            };

            await channel.QueueDeclareAsync(
                queue: queueConfig.EffectiveRetryQueueName,
                durable: queueConfig.QueueDurable,
                exclusive: false,
                autoDelete: queueConfig.QueueAutoDelete,
                arguments: retryQueueArguments,
                cancellationToken: stoppingToken).ConfigureAwait(false);

            await channel.QueueBindAsync(
                queue: queueConfig.EffectiveRetryQueueName,
                exchange: queueConfig.EffectiveRetryExchangeName,
                routingKey: queueConfig.EffectiveRetryRoutingKey,
                cancellationToken: stoppingToken).ConfigureAwait(false);

            await channel.ExchangeDeclareAsync(
                exchange: queueConfig.EffectiveDeadLetterExchangeName,
                type: ExchangeType.Direct,
                durable: queueConfig.ExchangeDurable,
                autoDelete: queueConfig.ExchangeAutoDelete,
                arguments: null,
                cancellationToken: stoppingToken).ConfigureAwait(false);

            await channel.QueueDeclareAsync(
                queue: queueConfig.EffectiveDeadLetterQueueName,
                durable: queueConfig.QueueDurable,
                exclusive: false,
                autoDelete: queueConfig.QueueAutoDelete,
                arguments: null,
                cancellationToken: stoppingToken).ConfigureAwait(false);

            await channel.QueueBindAsync(
                queue: queueConfig.EffectiveDeadLetterQueueName,
                exchange: queueConfig.EffectiveDeadLetterExchangeName,
                routingKey: queueConfig.EffectiveDeadLetterRoutingKey,
                cancellationToken: stoppingToken).ConfigureAwait(false);
        }
 
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
            // Poison messages are dropped (acked) — they would never succeed anyway.
            TRow row;
            try
            {
                TMessage message = Deserialize(ea.Body)
                    ?? throw new InvalidOperationException("Deserialized payload was null.");
                row = Map(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Consumer}: failed to deserialize/map message (deliveryTag={Tag})",
                    ConsumerName, ea.DeliveryTag);
                await RouteToDeadLetterOrAckAsync(
                        channel,
                        ea,
                        queueConfig,
                        stoppingToken,
                        "deserialization_failed")
                    .ConfigureAwait(false);
                return;
            }
 
            await pendingLock.WaitAsync(stoppingToken).ConfigureAwait(false);
            try
            {
                pending.Add(new PendingDelivery<TRow>(ea, row));
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
 
                foreach (PendingDelivery<TRow> item in snapshot)
                {
                    await channel.BasicAckAsync(item.Delivery.DeliveryTag, multiple: false, cancellationToken: stoppingToken)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "{Consumer}: Postgres flush failed for {Count} messages, pausing consumer",
                    ConsumerName, snapshot.Count);

                if (queueConfig.IsRetryEnabled)
                {
                    await HandleBatchFailureWithRetryAsync(
                            channel,
                            queueConfig,
                            snapshot,
                            stoppingToken)
                        .ConfigureAwait(false);
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
 
    private async Task HandleBatchFailureWithRetryAsync(
        IChannel channel,
        RabbitMqQueueConfigModel queueConfig,
        IReadOnlyList<PendingDelivery<TRow>> snapshot,
        CancellationToken cancellationToken)
    {
        int maxRetries = queueConfig.MaxRetries > 0 ? queueConfig.MaxRetries : 5;
        foreach (PendingDelivery<TRow> pendingDelivery in snapshot)
        {
            int retryCount = GetRetryCount(pendingDelivery.Delivery, QueueName);
            if (retryCount >= maxRetries)
            {
                await PublishToDeadLetterAndAckAsync(
                        channel,
                        pendingDelivery.Delivery,
                        queueConfig,
                        cancellationToken,
                        "max_retry_reached")
                    .ConfigureAwait(false);
                continue;
            }

            await channel.BasicNackAsync(
                    pendingDelivery.Delivery.DeliveryTag,
                    multiple: false,
                    requeue: false,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }

    private async Task RouteToDeadLetterOrAckAsync(
        IChannel channel,
        BasicDeliverEventArgs delivery,
        RabbitMqQueueConfigModel queueConfig,
        CancellationToken cancellationToken,
        string reason)
    {
        try
        {
            if (queueConfig.IsRetryEnabled)
            {
                await PublishToDeadLetterAndAckAsync(channel, delivery, queueConfig, cancellationToken, reason)
                    .ConfigureAwait(false);
            }
            else
            {
                await channel.BasicAckAsync(delivery.DeliveryTag, multiple: false, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
        }
        catch
        {
            // channel may be gone — next session will reconnect
        }
    }

    private async Task PublishToDeadLetterAndAckAsync(
        IChannel channel,
        BasicDeliverEventArgs delivery,
        RabbitMqQueueConfigModel queueConfig,
        CancellationToken cancellationToken,
        string reason)
    {
        BasicProperties deadLetterProperties = new()
        {
            ContentType = "application/json",
            DeliveryMode = DeliveryModes.Persistent,
            Headers = CloneHeaders(delivery.BasicProperties.Headers)
        };

        int retryCount = GetRetryCount(delivery, QueueName);
        deadLetterProperties.Headers["x-retry-count"] = retryCount;
        deadLetterProperties.Headers["x-failure-reason"] = reason;

        await channel.BasicPublishAsync(
                exchange: queueConfig.EffectiveDeadLetterExchangeName,
                routingKey: queueConfig.EffectiveDeadLetterRoutingKey,
                mandatory: false,
                basicProperties: deadLetterProperties,
                body: delivery.Body,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        await channel.BasicAckAsync(delivery.DeliveryTag, multiple: false, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    private static int GetRetryCount(BasicDeliverEventArgs delivery, string queueName)
    {
        int headerCount = TryGetIntValue(delivery.BasicProperties.Headers, "x-retry-count");
        int deadLetterCount = GetDeadLetterCount(delivery.BasicProperties.Headers, queueName);
        return Math.Max(headerCount, deadLetterCount);
    }

    private static int TryGetIntValue(IDictionary<string, object?>? headers, string key)
    {
        if (headers is null || !headers.TryGetValue(key, out object? value) || value is null)
        {
            return 0;
        }

        if (value is byte[] bytes && int.TryParse(System.Text.Encoding.UTF8.GetString(bytes), out int byteValue))
        {
            return byteValue;
        }

        if (value is ReadOnlyMemory<byte> memory &&
            int.TryParse(System.Text.Encoding.UTF8.GetString(memory.Span), out int memoryValue))
        {
            return memoryValue;
        }

        return value switch
        {
            int intValue => intValue,
            long longValue => (int)longValue,
            short shortValue => shortValue,
            byte byteValue2 => byteValue2,
            _ => int.TryParse(value.ToString(), out int parsedValue) ? parsedValue : 0
        };
    }

    private static int GetDeadLetterCount(IDictionary<string, object?>? headers, string queueName)
    {
        if (headers is null || !headers.TryGetValue("x-death", out object? value) || value is null)
        {
            return 0;
        }

        if (value is not System.Collections.IList deathEntries)
        {
            return 0;
        }

        int retries = 0;
        foreach (object? entry in deathEntries)
        {
            if (entry is not IDictionary<string, object?> deathDictionary)
            {
                continue;
            }

            object? queue = deathDictionary.TryGetValue("queue", out object? queueObj) ? queueObj : null;
            string? queueValue = queue as string ?? (queue is ReadOnlyMemory<byte> queueMem
                ? System.Text.Encoding.UTF8.GetString(queueMem.Span)
                : queue is byte[] queueBytes ? System.Text.Encoding.UTF8.GetString(queueBytes) : null);

            if (!string.Equals(queueValue, queueName, StringComparison.Ordinal))
            {
                continue;
            }

            retries = Math.Max(retries, TryGetIntValue(deathDictionary, "count"));
        }

        return retries;
    }

    private static Dictionary<string, object?> CloneHeaders(IDictionary<string, object?>? headers)
    {
        if (headers is null)
        {
            return [];
        }

        return headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    private readonly record struct PendingDelivery<T>(BasicDeliverEventArgs Delivery, T Row);

}
