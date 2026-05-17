using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.infra.PostgreSQL.Abstractions;

public interface IHttpTrafficLogQueue
{
    bool TryEnqueue(HttpTrafficLogItem item);
    ValueTask<HttpTrafficLogItem> DequeueAsync(CancellationToken cancellationToken);
}
