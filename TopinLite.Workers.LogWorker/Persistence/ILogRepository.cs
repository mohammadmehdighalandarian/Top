namespace TopinLite.Workers.LogWorker.Persistence;

public interface ILogRepository
{
    Task BulkInsertUserActivityAsync(IReadOnlyCollection<UserActivityLogRow> rows, CancellationToken cancellationToken = default);
    Task BulkInsertErrorsAsync(IReadOnlyCollection<ErrorLogRow> rows, CancellationToken cancellationToken = default);
}