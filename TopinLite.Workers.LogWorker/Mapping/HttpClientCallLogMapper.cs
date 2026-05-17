using System.Net;
using TopinLite.Domain.LogModels;
using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.Workers.LogWorker.Mapping;

internal static class HttpClientCallLogMapper
{
    /// <summary>
    /// Translate the outbound-HTTP log payload that microservices publish to
    /// RabbitMQ into the same Request/Response shape the inbound middleware
    /// produces. We treat each log entry as a paired request+response captured
    /// at a single point in time on the calling side.
    /// </summary>
    public static HttpTrafficLogItem ToTrafficItem(
        HttpClientCallLogModel model,
        string serverName,
        string environmentName,
        DateTimeOffset receivedAt)
    {
        Guid requestId = Guid.NewGuid();
        DateTimeOffset responseTime = receivedAt;
        DateTimeOffset requestTime = receivedAt - TimeSpan.FromMilliseconds(Math.Max(0, model.ElapsedMilliseconds));

        UrlParts parts = ParseUrl(model.Url);

        var requestLog = new HttpRequestLog
        {
            Id = requestId,
            RequestTime = requestTime,
            ServerName = serverName,
            EnvironmentName = environmentName,
            // Direction marker: outbound HTTP from `From` system. We don't
            // have the verb on the wire so we record an explicit sentinel
            // that's easy to filter on later.
            Method = "OUTBOUND",
            Scheme = parts.Scheme,
            Host = parts.Host,
            Port = parts.Port,
            Path = parts.Path ?? string.Empty,
            QueryString = parts.QueryString,
            RequestHeadersJson = NullIfEmpty(model.RequestHeader),
            RequestBody = model.RequestBody,
            AppUser = model.From
        };

        var responseLog = new HttpResponseLog
        {
            Id = Guid.NewGuid(),
            RequestId = requestId,
            RequestTime = requestTime,
            ResponseTime = responseTime,
            ServerName = serverName,
            EnvironmentName = environmentName,
            StatusCode = model.HttpStatusCode,
            DurationMs = Math.Max(0, model.ElapsedMilliseconds),
            ResponseHeadersJson = NullIfEmpty(model.ResponseHeader),
            ResponseBody = model.ResponseBody,
            IsSuccess = model.HttpStatusCode is >= 200 and < 400
        };

        return new HttpTrafficLogItem { Request = requestLog, Response = responseLog };
    }

    private static string? NullIfEmpty(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;

    private readonly record struct UrlParts(string? Scheme, string? Host, int? Port, string? Path, string? QueryString);

    private static UrlParts ParseUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return new UrlParts(null, null, null, null, null);
        }

        // Try absolute first; fall back to relative so path-only URLs still log.
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri? absolute))
        {
            return new UrlParts(
                absolute.Scheme,
                absolute.Host,
                absolute.IsDefaultPort ? null : absolute.Port,
                absolute.AbsolutePath,
                absolute.Query is { Length: > 0 } q ? q : null);
        }

        // Relative or malformed — keep what we got, leave host/port null.
        int qIdx = url.IndexOf('?');
        if (qIdx >= 0)
        {
            return new UrlParts(null, null, null, url[..qIdx], url[qIdx..]);
        }

        return new UrlParts(null, null, null, url, null);
    }
}