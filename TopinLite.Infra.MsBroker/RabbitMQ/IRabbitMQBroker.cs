namespace TopinLite.Infra.MsBroker.RabbitMQ
{
    public interface IRabbitMQBroker
    {
        bool PartyLogger(PartyCallLoggingModel model);
    }
}
