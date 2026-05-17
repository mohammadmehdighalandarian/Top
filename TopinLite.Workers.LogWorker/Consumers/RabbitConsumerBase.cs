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

    /// <summary>The RabbitMQ queue this consumer reads from.</summary>
    protected abstract string QueueName { get; }
 
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
        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken).ConfigureAwait(false);
 
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
                _logger.LogError(ex,
                    "{Consumer}: dropping unparseable message (deliveryTag={Tag})",
                    ConsumerName, ea.DeliveryTag);
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
                pending.Add(new PendingDelivery<TRow>(ea.DeliveryTag, row));
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
                    "{Consumer}: Postgres flush failed for {Count} messages, pausing consumer",
                    ConsumerName, snapshot.Count);
 
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
 
    private readonly record struct PendingDelivery<T>(ulong DeliveryTag, T Row);

}