using Microsoft.Extensions.Options;
using TopinLite.Domain.Configuration;
using TopinLite.Domain.LogModels;

namespace TopinLite.Infra.MsBroker.RabbitMQ.Loggers
{
    public class RabbitMqBroker : IRabbitMQBroker
    {
        private readonly IProducer _producer;
        private readonly RabbitMqConfigModel _rabbitConfig;
        
        public RabbitMqBroker(IProducer producer, IOptions<RabbitMqConfigModel> config)
        {
            _rabbitConfig = config.Value;
            _producer = producer;
        }
        public async Task HttpCallLog(HttpClientCallLogModel model, CancellationToken token)
        {
            await _producer.SendMessage(model, _rabbitConfig.HttpCallLog);
        }

        public async Task UserActivityCallLog(UserActivityCallLogModel model, CancellationToken token)
        {
            await _producer.SendMessage(model, _rabbitConfig.UserActivityLog);
        }

        public async Task ErrorCallLog(ErrorLogModel model, CancellationToken token)
        {
            await _producer.SendMessage(model, _rabbitConfig.ErrorLog);
        }
    }
}