namespace TopinLite.Workers.LogWorker.Configuration;

public sealed class LogConsumerOptions
{
    public const string SectionName = "LogConsumer";

    /// <summary>
    /// Max unacked messages held by RabbitMQ per consumer. Bounds memory.
    /// </summary>
    public ushort PrefetchCount { get; set; } = 200;

    /// <summary>
    /// Max items per Postgres batch insert. Should be &lt;= PrefetchCount.
    /// </summary>
    public int BatchSize { get; set; } = 200;

    /// <summary>
    /// Max wait time before flushing a partial batch.
    /// </summary>
    public int FlushIntervalMilliseconds { get; set; } = 1000;

    /// <summary>
    /// Wait time before retrying after a Postgres failure. Messages stay
    /// unacked in RabbitMQ during this period (per user requirement).
    /// </summary>
    public int PostgresRetryDelayMilliseconds { get; set; } = 5000;

    /// <summary>
    /// Wait time before reconnecting after a RabbitMQ connection failure.
    /// </summary>
    public int RabbitReconnectDelayMilliseconds { get; set; } = 5000;

    /// <summary>
    /// Server name written to log rows (defaults to machine name).
    /// </summary>
    public string ServerName { get; set; } = Environment.MachineName;

    /// <summary>
    /// Environment label written to log rows.
    /// </summary>
    public string EnvironmentName { get; set; } = "Production";
}