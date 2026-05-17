namespace TopinLite.Workers.LogWorker.Persistence;

public sealed class UserActivityLogRow
{
    public DateTimeOffset EventAt { get; set; }
    public string? ServerName { get; set; }
    public string? EnvironmentName { get; set; }
    public string? TraceIdentifier { get; set; }
    public short Direction { get; set; }
    public string? Verb { get; set; }
    public string? Path { get; set; }
    public int StatusCode { get; set; }
    public string? Body { get; set; }
    public string? HeadersJson { get; set; }
    public string? ConnectionInfo { get; set; }
    public string? ConsumerKey { get; set; }
    public string? Origin { get; set; }
}

public sealed class ErrorLogRow
{
    public string? ServerName { get; set; }
    public string? EnvironmentName { get; set; }
    public string? RequestPath { get; set; }
    public string? QueryParam { get; set; }
    public string? RequestHeadersJson { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? StackTrace { get; set; }
    public string? ExceptionJson { get; set; }
}