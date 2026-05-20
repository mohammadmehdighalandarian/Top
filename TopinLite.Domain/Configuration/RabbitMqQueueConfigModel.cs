namespace TopinLite.Domain.Configuration;

public class RabbitMqQueueConfigModel
{
    public string ExchangeName { get; set; }
    public string QueueName { get; set; }
    public string RoutingKey { get; set; }
    public bool QueueDurable { get; set; }
    public bool QueueAutoDelete { get; set; }
    public bool ExchangeDurable { get; set; }
    public bool ExchangeAutoDelete { get; set; }
    public bool EnableRetry { get; set; }
    public int MaxRetries { get; set; } = 5;
    public int RetryDelayMilliseconds { get; set; } = 30000;
    public string RetryExchangeName { get; set; }
    public string RetryQueueName { get; set; }
    public string RetryRoutingKey { get; set; }
    public string DeadLetterExchangeName { get; set; }
    public string DeadLetterQueueName { get; set; }
    public string DeadLetterRoutingKey { get; set; }

    public bool IsRetryEnabled => EnableRetry;

    public string EffectiveRetryExchangeName => string.IsNullOrWhiteSpace(RetryExchangeName)
        ? $"{ExchangeName}.retry"
        : RetryExchangeName;

    public string EffectiveRetryQueueName => string.IsNullOrWhiteSpace(RetryQueueName)
        ? $"{QueueName}.retry"
        : RetryQueueName;

    public string EffectiveRetryRoutingKey => string.IsNullOrWhiteSpace(RetryRoutingKey)
        ? $"{RoutingKey}.retry"
        : RetryRoutingKey;

    public string EffectiveDeadLetterExchangeName => string.IsNullOrWhiteSpace(DeadLetterExchangeName)
        ? $"{ExchangeName}.dlq"
        : DeadLetterExchangeName;

    public string EffectiveDeadLetterQueueName => string.IsNullOrWhiteSpace(DeadLetterQueueName)
        ? $"{QueueName}.dlq"
        : DeadLetterQueueName;

    public string EffectiveDeadLetterRoutingKey => string.IsNullOrWhiteSpace(DeadLetterRoutingKey)
        ? $"{RoutingKey}.dlq"
        : DeadLetterRoutingKey;
}
