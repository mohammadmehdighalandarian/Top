using TopinLite.Domain.LogModels;

namespace TopinLite.Infra.MsBroker.RabbitMQ
{
    public interface IRabbitMQBroker
    {
        Task HttpCallLog(HttpClientCallLogModel model, CancellationToken token);
        Task UserActivityCallLog(UserActivityCallLogModel model, CancellationToken token);
        Task ErrorCallLog(ErrorLogModel model, CancellationToken token);
    }
}
