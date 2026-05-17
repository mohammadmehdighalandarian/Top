namespace TopinLite.infra.PostgreSQL.Models;

public sealed class HttpTrafficLogItem
{
    public required HttpRequestLog Request { get; init; }
    public required HttpResponseLog Response { get; init; }
}
