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
}