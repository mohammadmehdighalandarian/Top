using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TopinLite.Domain.Configuration;
using TopinLite.Domain.LogModels;
using TopinLite.Workers.LogWorker.Configuration;
using TopinLite.Workers.LogWorker.Mapping;
using TopinLite.Workers.LogWorker.Persistence;

namespace TopinLite.Workers.LogWorker.Consumers;

internal sealed class ErrorLogConsumer : RabbitConsumerBase<ErrorLogModel, ErrorLogRow>
{
    private readonly ILogRepository _repository;
    private readonly RabbitMqQueueConfigModel _queueConfig;
    private readonly LogConsumerOptions _options;

    public ErrorLogConsumer(
        IOptions<RabbitMqConfigModel> rabbitOptions,
        IOptions<LogConsumerOptions> consumerOptions,
        ILogRepository repository,
        ILogger<ErrorLogConsumer> logger)
        : base(rabbitOptions, consumerOptions, logger)
    {
        _repository = repository;
        _queueConfig = rabbitOptions.Value.ErrorLog;
        _options = consumerOptions.Value;
    }

    protected override string QueueName => _queueConfig.QueueName;
    protected override string ConsumerName => "ErrorLog";

    protected override ErrorLogRow Map(ErrorLogModel message)
        => ErrorLogMapper.ToRow(message, _options.ServerName, _options.EnvironmentName);

    protected override Task FlushAsync(IReadOnlyList<ErrorLogRow> batch, CancellationToken cancellationToken)
        => _repository.BulkInsertErrorsAsync(batch, cancellationToken);
}