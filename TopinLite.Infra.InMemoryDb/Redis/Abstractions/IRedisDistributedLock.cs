namespace TopinLite.Infra.InMemoryDb.Redis.Abstractions
{
    public interface IRedisDistributedLock
    {
        Task<IAsyncDisposable?> TryAcquireAsync(
            string key,
            TimeSpan expiry,
            CancellationToken cancellationToken = default);
    }
}
