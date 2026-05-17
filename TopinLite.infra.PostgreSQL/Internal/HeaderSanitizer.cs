using System.Text.Json;

using Microsoft.AspNetCore.Http;

using TopinLite.infra.PostgreSQL.Configuration;

namespace TopinLite.infra.PostgreSQL.Internal;

internal static class HeaderSanitizer
{
    public static string? ToJson(IHeaderDictionary headers, PostgresTrafficOptions options)
    {
        if (!options.CaptureHeaders)
        {
            return null;
        }

        var sensitive = new HashSet<string>(options.SensitiveHeaders ?? [], StringComparer.OrdinalIgnoreCase);
        var result = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        foreach (var header in headers)
        {
            result[header.Key] = sensitive.Contains(header.Key)
                ? "***REDACTED***"
                : header.Value.ToString();
        }

        return JsonSerializer.Serialize(result);
    }
}
