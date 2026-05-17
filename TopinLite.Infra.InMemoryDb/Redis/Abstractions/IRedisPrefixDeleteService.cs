namespace TopinLite.Infra.InMemoryDb.Redis.Abstractions
{
    public interface IRedisPrefixDeleteService
    {
        Task<long> DeleteByPrefixAsync(
            string prefix,
            CancellationToken cancellationToken = default);
    }
}
