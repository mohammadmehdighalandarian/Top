using TopinLite.Infra.InMemoryDb.Redis.Models;

namespace TopinLite.Infra.InMemoryDb.Redis.Abstractions
{
    public interface IRedisPrefixDeleteQueue
    {
        ValueTask QueueAsync(PrefixDeleteRequest request, CancellationToken cancellationToken = default);
    }
}
