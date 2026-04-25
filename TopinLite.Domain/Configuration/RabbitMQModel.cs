namespace TopinLite.Domain.Configuration
{
    public class RabbitMQConfigurationModel
    {
        public string HostAddress { get; set; }
        public int? Port { get => field ?? 5672; set => field = value; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public bool? Durable { get => field ?? false; set => field = value; }
        public bool? AutoDelete { get => field ?? false; set => field = value; }
        public string ClientName { get => field ?? ".Net Application"; set => field = value; }
        public bool? CreateQueue { get => field ?? false; set => field = value; }
        public bool? CreateExchange { get => field ?? false; set => field = value; }
        public bool? BindQueueToExchange { get => field ?? false; set => field = value; }
    }


}