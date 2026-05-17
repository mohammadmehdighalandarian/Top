using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TopinLite.Domain.Configuration;
using TopinLite.Domain.LogModels;
using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Models;
using TopinLite.Workers.LogWorker.Configuration;
using TopinLite.Workers.LogWorker.Mapping;

namespace TopinLite.Workers.LogWorker.Consumers;

internal sealed class HttpCallLogConsumer : RabbitConsumerBase<HttpClientCallLogModel, HttpTrafficLogItem>
{
    private readonly IHttpTrafficRepository _repository;
    private readonly RabbitMqQueueConfigModel _queueConfig;
    private readonly LogConsumerOptions _options;

    public HttpCallLogConsumer(
        IOptions<RabbitMqConfigModel> rabbitOptions,
        IOptions<LogConsumerOptions> consumerOptions,
        IHttpTrafficRepository repository,
        ILogger<HttpCallLogConsumer> logger)
        : base(rabbitOptions, consumerOptions, logger)
    {
        _repository = repository;
        _queueConfig = rabbitOptions.Value.HttpCallLog;
        _options = consumerOptions.Value;
    }

    protected override string QueueName => _queueConfig.QueueName;
    protected override string ConsumerName => "HttpCallLog";

    protected override HttpTrafficLogItem Map(HttpClientCallLogModel message)
        => HttpClientCallLogMapper.ToTrafficItem(
            message,
            _options.ServerName,
            _options.EnvironmentName,
            DateTimeOffset.UtcNow);

    protected override Task FlushAsync(IReadOnlyList<HttpTrafficLogItem> batch, CancellationToken cancellationToken)
        => _repository.BulkInsertTrafficAsync(batch, cancellationToken);
}