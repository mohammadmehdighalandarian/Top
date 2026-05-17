using System.Diagnostics;

using TopinLite.infra.PostgreSQL.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Configuration;
using TopinLite.infra.PostgreSQL.Internal;

namespace TopinLite.infra.PostgreSQL.Middleware;

public sealed class HttpTrafficLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpTrafficLogQueue _queue;
    private readonly PostgresTrafficOptions _options;

    public HttpTrafficLoggingMiddleware(
        RequestDelegate next,
        IHttpTrafficLogQueue queue,
        IOptions<PostgresTrafficOptions> options)
    {
        _next = next;
        _queue = queue;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (ShouldSkip(context.Request.Path))
        {
            await _next(context).ConfigureAwait(false);
            return;
        }

        var started = Stopwatch.GetTimestamp();
        var requestId = Guid.NewGuid();
        var requestTime = DateTimeOffset.UtcNow;
        var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;
        var correlationId = GetHeader(context, "X-Correlation-ID") ?? GetHeader(context, "X-Request-ID") ?? traceId;

        string? requestBody = null;
        if (_options.CaptureRequestBody && CanCaptureBody(context.Request.ContentType))
        {
            requestBody = await BodyCaptureHelper.ReadRequestBodyAsync(
                context.Request,
                _options.MaxRequestBodyBytes,
                context.RequestAborted).ConfigureAwait(false);
        }

        var requestLog = new HttpRequestLog
        {
            Id = requestId,
            RequestTime = requestTime,
            TraceId = traceId,
            CorrelationId = correlationId,
            ServerName = _options.ServerName,
            EnvironmentName = _options.EnvironmentName,
            Method = context.Request.Method,
            Scheme = context.Request.Scheme,
            Host = context.Request.Host.Host,
            Port = context.Request.Host.Port,
            Path = context.Request.Path.Value ?? string.Empty,
            QueryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : null,
            ClientIp = context.Connection.RemoteIpAddress?.ToString(),
            XForwardedFor = GetHeader(context, "X-Forwarded-For"),
            UserAgent = GetHeader(context, "User-Agent"),
            Referer = GetHeader(context, "Referer"),
            ContentType = context.Request.ContentType,
            ContentLength = context.Request.ContentLength,
            RequestHeadersJson = HeaderSanitizer.ToJson(context.Request.Headers, _options),
            RequestBody = requestBody,
            AppUser = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : null,
            TenantId = GetHeader(context, "X-Tenant-ID")
        };

        var originalBody = context.Response.Body;
        await using var responseBuffer = new MemoryStream();
        context.Response.Body = responseBuffer;

        Exception? capturedException = null;

        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            capturedException = ex;
            throw;
        }
        finally
        {
            var elapsedMs = Stopwatch.GetElapsedTime(started).TotalMilliseconds;

            string? responseBody = null;
            if (_options.CaptureResponseBody && CanCaptureBody(context.Response.ContentType))
            {
                responseBody = await BodyCaptureHelper.ReadResponseBodyAsync(
                    responseBuffer,
                    _options.MaxResponseBodyBytes,
                    CancellationToken.None).ConfigureAwait(false);
            }

            responseBuffer.Position = 0;
            await responseBuffer.CopyToAsync(originalBody).ConfigureAwait(false);
            context.Response.Body = originalBody;

            var responseLog = new HttpResponseLog
            {
                Id = Guid.NewGuid(),
                RequestTime = requestTime,
                ResponseTime = DateTimeOffset.UtcNow,
                RequestId = requestId,
                TraceId = traceId,
                CorrelationId = correlationId,
                ServerName = _options.ServerName,
                EnvironmentName = _options.EnvironmentName,
                StatusCode = context.Response.StatusCode,
                DurationMs = (long)Math.Round(elapsedMs),
                ContentType = context.Response.ContentType,
                ContentLength = context.Response.ContentLength,
                ResponseHeadersJson = HeaderSanitizer.ToJson(context.Response.Headers, _options),
                ResponseBody = responseBody,
                ErrorMessage = capturedException?.Message,
                ExceptionType = capturedException?.GetType().FullName,
                IsSuccess = capturedException is null && context.Response.StatusCode < 500
            };

            _queue.TryEnqueue(new HttpTrafficLogItem
            {
                Request = requestLog,
                Response = responseLog
            });
        }
    }

    private bool ShouldSkip(PathString path)
    {
        var value = path.Value ?? string.Empty;
        return _options.ExcludedPaths.Any(x => value.StartsWith(x, StringComparison.OrdinalIgnoreCase));
    }

    private static string? GetHeader(HttpContext context, string headerName)
    {
        return context.Request.Headers.TryGetValue(headerName, out var value) ? value.ToString() : null;
    }

    private static bool CanCaptureBody(string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
        {
            return true;
        }

        return contentType.Contains("json", StringComparison.OrdinalIgnoreCase)
            || contentType.Contains("text", StringComparison.OrdinalIgnoreCase)
            || contentType.Contains("xml", StringComparison.OrdinalIgnoreCase)
            || contentType.Contains("x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase);
    }
}
