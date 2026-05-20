using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TopinLite.Domain.Configuration;
using TopinLite.Domain.LogModels;
using TopinLite.Workers.LogWorker.Configuration;
using TopinLite.Workers.LogWorker.Mapping;
using TopinLite.Workers.LogWorker.Persistence;

namespace TopinLite.Workers.LogWorker.Consumers;

internal sealed class UserActivityLogConsumer : RabbitConsumerBase<UserActivityCallLogModel, UserActivityLogRow>
{
    private readonly ILogRepository _repository;
    private readonly RabbitMqQueueConfigModel _queueConfig;
    private readonly LogConsumerOptions _options;

    public UserActivityLogConsumer(
        IOptions<RabbitMqConfigModel> rabbitOptions,
        IOptions<LogConsumerOptions> consumerOptions,
        ILogRepository repository,
        ILogger<UserActivityLogConsumer> logger)
        : base(rabbitOptions, consumerOptions, logger)
    {
        _repository = repository;
        _queueConfig = rabbitOptions.Value.UserActivityLog;
        _options = consumerOptions.Value;
    }

    protected override RabbitMqQueueConfigModel QueueConfig => _queueConfig;
    protected override string ConsumerName => "UserActivityLog";

    protected override UserActivityLogRow Map(UserActivityCallLogModel message)
        => UserActivityLogMapper.ToRow(message, _options.ServerName, _options.EnvironmentName);

    protected override Task FlushAsync(IReadOnlyList<UserActivityLogRow> batch, CancellationToken cancellationToken)
        => _repository.BulkInsertUserActivityAsync(batch, cancellationToken);
}