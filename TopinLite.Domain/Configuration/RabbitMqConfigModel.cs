namespace TopinLite.Domain.Configuration;

public class RabbitMqConfigModel
{
    public RabbitMqConnectionConfigModel RabbitConnection { get; set; }
    public RabbitMqQueueConfigModel HttpCallLog { get; set; }
    public RabbitMqQueueConfigModel UserActivityLog { get; set; }
    public RabbitMqQueueConfigModel ErrorLog { get; set; }
}