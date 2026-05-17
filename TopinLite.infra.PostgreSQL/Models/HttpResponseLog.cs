namespace TopinLite.infra.PostgreSQL.Models;

public sealed class HttpResponseLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset RequestTime { get; set; }
    public DateTimeOffset ResponseTime { get; set; }

    public Guid? RequestId { get; set; }
    public string? TraceId { get; set; }
    public string? CorrelationId { get; set; }
    public string? ServerName { get; set; }
    public string? EnvironmentName { get; set; }

    public int StatusCode { get; set; }
    public long DurationMs { get; set; }

    public string? ContentType { get; set; }
    public long? ContentLength { get; set; }

    public string? ResponseHeadersJson { get; set; }
    public string? ResponseBody { get; set; }

    public string? ErrorMessage { get; set; }
    public string? ExceptionType { get; set; }
    public bool IsSuccess { get; set; }
}
