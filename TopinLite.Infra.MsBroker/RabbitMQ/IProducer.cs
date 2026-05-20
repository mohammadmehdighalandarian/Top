using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TopinLite.Domain.Configuration;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Infra.MsBroker.RabbitMQ
{
    public interface IProducer
    {
        Task<bool> SendMessage(object model, RabbitMqQueueConfigModel queueConfig);
    }

    public class Producer : IProducer, IDisposable
    {
        private readonly RabbitMqConfigModel _envConfig;
        private readonly ILogger<Producer> _logger;
        private IConnection _connection;
        private IChannel _model;
        private bool _disposed;
        
        public Producer(IOptions<RabbitMqConfigModel> envConfig, ILogger<Producer> logger)
        {
            _envConfig = envConfig.Value;
            _logger = logger;

            _ = CreateRabbitConnection();
        }
        
        private async Task CreateRabbitConnection()
        {
            try
            {
                ConnectionFactory factory = new()
                {
                    HostName = _envConfig.RabbitConnection.HostAddress,
                    UserName = _envConfig.RabbitConnection.Username,
                    Password = _envConfig.RabbitConnection.Password,
                    Port = _envConfig.RabbitConnection.PortNumber,
                    ClientProvidedName = _envConfig.RabbitConnection.ClientName,
                    AutomaticRecoveryEnabled = true,
                    TopologyRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
                };

                _connection = await factory.CreateConnectionAsync();
                _model = await _connection.CreateChannelAsync();
                
                if (_envConfig.HttpCallLog != null) await CreateExchangeAndQueue(_envConfig.HttpCallLog);
                if (_envConfig.UserActivityLog != null) await CreateExchangeAndQueue(_envConfig.UserActivityLog);
                if (_envConfig.ErrorLog != null) await CreateExchangeAndQueue(_envConfig.ErrorLog);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create RabbitMQ connection: {Message}", ex.Message);
                throw;
            }
        }
        
        private async Task CreateExchangeAndQueue(RabbitMqQueueConfigModel queueConfig)
        {
            try
            {
                Dictionary<string, object?> queueArguments = [];
                if (queueConfig.IsRetryEnabled)
                {
                    queueArguments["x-dead-letter-exchange"] = queueConfig.EffectiveRetryExchangeName;
                    queueArguments["x-dead-letter-routing-key"] = queueConfig.EffectiveRetryRoutingKey;
                }

                await _model.QueueDeclareAsync(
                    queue: queueConfig.QueueName,
                    durable: queueConfig.QueueDurable,
                    exclusive: false,
                    autoDelete: queueConfig.QueueAutoDelete,
                    arguments: queueArguments.Count == 0 ? null : queueArguments);
                await _model.ExchangeDeclareAsync(
                    exchange: queueConfig.ExchangeName,
                    type: ExchangeType.Direct,
                    durable: queueConfig.ExchangeDurable,
                    autoDelete: queueConfig.ExchangeAutoDelete,
                    arguments: null);
                await _model.QueueBindAsync(
                    queue: queueConfig.QueueName,
                    exchange: queueConfig.ExchangeName,
                    routingKey: queueConfig.RoutingKey);

                if (!queueConfig.IsRetryEnabled)
                {
                    return;
                }

                await _model.ExchangeDeclareAsync(
                    exchange: queueConfig.EffectiveRetryExchangeName,
                    type: ExchangeType.Direct,
                    durable: queueConfig.ExchangeDurable,
                    autoDelete: queueConfig.ExchangeAutoDelete,
                    arguments: null);

                Dictionary<string, object?> retryQueueArguments = new()
                {
                    ["x-message-ttl"] = queueConfig.RetryDelayMilliseconds > 0 ? queueConfig.RetryDelayMilliseconds : 30000,
                    ["x-dead-letter-exchange"] = queueConfig.ExchangeName,
                    ["x-dead-letter-routing-key"] = queueConfig.RoutingKey
                };

                await _model.QueueDeclareAsync(
                    queue: queueConfig.EffectiveRetryQueueName,
                    durable: queueConfig.QueueDurable,
                    exclusive: false,
                    autoDelete: queueConfig.QueueAutoDelete,
                    arguments: retryQueueArguments);

                await _model.QueueBindAsync(
                    queue: queueConfig.EffectiveRetryQueueName,
                    exchange: queueConfig.EffectiveRetryExchangeName,
                    routingKey: queueConfig.EffectiveRetryRoutingKey);

                await _model.ExchangeDeclareAsync(
                    exchange: queueConfig.EffectiveDeadLetterExchangeName,
                    type: ExchangeType.Direct,
                    durable: queueConfig.ExchangeDurable,
                    autoDelete: queueConfig.ExchangeAutoDelete,
                    arguments: null);

                await _model.QueueDeclareAsync(
                    queue: queueConfig.EffectiveDeadLetterQueueName,
                    durable: queueConfig.QueueDurable,
                    exclusive: false,
                    autoDelete: queueConfig.QueueAutoDelete,
                    arguments: null);

                await _model.QueueBindAsync(
                    queue: queueConfig.EffectiveDeadLetterQueueName,
                    exchange: queueConfig.EffectiveDeadLetterExchangeName,
                    routingKey: queueConfig.EffectiveDeadLetterRoutingKey);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create exchange or queue: {Message}", ex.Message);
                throw;
            }
        }
        
        public async Task<bool> SendMessage(object model, RabbitMqQueueConfigModel queueConfig)
        {
            try
            {
                byte[] message = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(model));
                if (_connection is not { IsOpen: true } || _model == null)
                {
                    await CreateRabbitConnection();
                }

                await CreateExchangeAndQueue(queueConfig);

                BasicProperties properties = new()
                {
                    ContentType = "application/json",
                    DeliveryMode = DeliveryModes.Persistent,
                    Headers = new Dictionary<string, object?>
                    {
                        ["x-retry-count"] = 0
                    }
                };

                await _model.BasicPublishAsync(
                    exchange: queueConfig.ExchangeName,
                    routingKey: queueConfig.RoutingKey,
                    mandatory: false,
                    basicProperties: properties,
                    body: message);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send message: {Message}", ex.Message);
                WriteLogFile.LogFile("Sending message by rabbit error :", ex);
                return false;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _model?.CloseAsync();
            _model?.Dispose();
            _connection?.CloseAsync();
            _connection?.Dispose();
            _disposed = true;
        }
    }
}
