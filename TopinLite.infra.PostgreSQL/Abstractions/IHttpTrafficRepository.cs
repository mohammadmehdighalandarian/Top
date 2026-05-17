using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.infra.PostgreSQL.Abstractions;

public interface IHttpTrafficRepository
{
    Task InsertRequestAsync(HttpRequestLog request, CancellationToken cancellationToken = default);
    Task InsertResponseAsync(HttpResponseLog response, CancellationToken cancellationToken = default);

    Task BulkInsertRequestsAsync(IReadOnlyCollection<HttpRequestLog> requests, CancellationToken cancellationToken = default);
    Task BulkInsertResponsesAsync(IReadOnlyCollection<HttpResponseLog> responses, CancellationToken cancellationToken = default);

    Task BulkInsertTrafficAsync(IReadOnlyCollection<HttpTrafficLogItem> items, CancellationToken cancellationToken = default);
}
