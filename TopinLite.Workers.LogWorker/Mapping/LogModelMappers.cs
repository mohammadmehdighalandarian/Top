using System.Text.Json;
using TopinLite.Domain.LogModels;
using TopinLite.Workers.LogWorker.Persistence;

namespace TopinLite.Workers.LogWorker.Mapping;

internal static class UserActivityLogMapper
{
    public static UserActivityLogRow ToRow(
        UserActivityCallLogModel model,
        string serverName,
        string environmentName)
    {
        return new UserActivityLogRow
        {
            EventAt = DateTimeOffset.FromUnixTimeMilliseconds(
                model.EventDate > 0 ? model.EventDate : DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()),
            ServerName = serverName,
            EnvironmentName = environmentName,
            TraceIdentifier = Truncate(model.TraceIdentifier, 128),
            Direction = (short)model.Direction,
            Verb = Truncate(model.Verb, 16),
            Path = model.Path,
            StatusCode = model.StatusCode,
            Body = model.Body,
            HeadersJson = ResolveHeadersJson(model),
            ConnectionInfo = model.ConnectionInfo,
            ConsumerKey = Truncate(model.ConsumerKey, 256),
            Origin = Truncate(model.Origin, 256)
        };
    }

    private static string? ResolveHeadersJson(UserActivityCallLogModel model)
    {
        // The producing middleware sets HeadersString = JsonSerializer.Serialize(...)
        // but the data ALSO comes through as the raw `Headers` object in some paths.
        // Prefer the pre-serialized string when present.
        if (!string.IsNullOrWhiteSpace(model.HeadersString))
        {
            return model.HeadersString;
        }

        if (model.Headers is null)
        {
            return null;
        }

        try
        {
            return JsonSerializer.Serialize(model.Headers);
        }
        catch
        {
            return null;
        }
    }

    private static string? Truncate(string? value, int max)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= max ? value : value[..max];
    }
}

internal static class ErrorLogMapper
{
    public static ErrorLogRow ToRow(
        ErrorLogModel model,
        string serverName,
        string environmentName)
    {
        return new ErrorLogRow
        {
            ServerName = serverName,
            EnvironmentName = environmentName,
            RequestPath = model.HttpContextJson?.Path,
            QueryParam = model.HttpContextJson?.QueryParam,
            RequestHeadersJson = NullIfEmptyJson(model.HttpContextJson?.Header),
            ExceptionMessage = model.ExceptionMessage,
            StackTrace = model.StackTrace,
            ExceptionJson = NullIfEmptyJson(model.ExceptionJson)
        };
    }

    private static string? NullIfEmptyJson(string? value)
    {
        // Postgres jsonb rejects empty strings — give it null instead.
        return string.IsNullOrWhiteSpace(value) ? null : value;
    }
}