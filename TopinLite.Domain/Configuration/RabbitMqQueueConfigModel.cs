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
    public string RetryExchangeName { get; set; }
    public string RetryQueueName { get; set; }
    public string RetryRoutingKey { get; set; }
    public string DeadLetterExchangeName { get; set; }
    public string DeadLetterQueueName { get; set; }
    public string DeadLetterRoutingKey { get; set; }
    public int? RetryDelayMilliseconds { get; set; }
    public int? MaxRetryCount { get; set; }
}
