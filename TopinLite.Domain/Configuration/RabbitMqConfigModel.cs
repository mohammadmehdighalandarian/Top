namespace TopinLite.Domain.Configuration;

public class RabbitMqConfigModel
{
    public RabbitMqConnectionConfigModel RabbitConnection { get; set; }
    public int MaxRetries { get; set; } = 5;
    public int RetryDelayMilliseconds { get; set; } = 30000;
    public RabbitMqQueueConfigModel HttpCallLog { get; set; }
    public RabbitMqQueueConfigModel UserActivityLog { get; set; }
    public RabbitMqQueueConfigModel ErrorLog { get; set; }
}
