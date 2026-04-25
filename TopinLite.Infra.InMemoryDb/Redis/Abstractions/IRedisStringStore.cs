namespace TopinLite.Infra.InMemoryDb.Redis.Abstractions
{
    public interface IRedisStringStore
    {
        ValueTask<bool> SetAsync(
            string key,
            string value,
            TimeSpan? expiry = null,
            CancellationToken cancellationToken = default);

        ValueTask<string?> GetAsync(
            string key,
            CancellationToken cancellationToken = default);

        ValueTask<bool> DeleteAsync(
            string key,
            CancellationToken cancellationToken = default);

        ValueTask<bool> ExistsAsync(
            string key,
            CancellationToken cancellationToken = default);

        ValueTask<string?> GetOrSetAsync(
            string key,
            Func<CancellationToken, Task<string?>> factory,
            TimeSpan? expiry = null,
            CancellationToken cancellationToken = default);
    }
}
