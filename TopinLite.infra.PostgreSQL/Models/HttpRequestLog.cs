namespace TopinLite.infra.PostgreSQL.Models;

public sealed class HttpRequestLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset RequestTime { get; set; }
    public string? TraceId { get; set; }
    public string? CorrelationId { get; set; }
    public string? ServerName { get; set; }
    public string? EnvironmentName { get; set; }

    public string Method { get; set; } = string.Empty;
    public string? Scheme { get; set; }
    public string? Host { get; set; }
    public int? Port { get; set; }
    public string Path { get; set; } = string.Empty;
    public string? QueryString { get; set; }

    public string? ClientIp { get; set; }
    public string? XForwardedFor { get; set; }
    public string? UserAgent { get; set; }
    public string? Referer { get; set; }

    public string? ContentType { get; set; }
    public long? ContentLength { get; set; }

    public string? RequestHeadersJson { get; set; }
    public string? RequestBody { get; set; }

    public string? AppUser { get; set; }
    public string? TenantId { get; set; }
}
