namespace TopinLite.infra.PostgreSQL.Configuration;

public sealed class PostgresTrafficOptions
{
    public const string SectionName = "PostgresTraffic";

    public string ConnectionString { get; set; } = string.Empty;

    public string ServerName { get; set; } = Environment.MachineName;

    public string EnvironmentName { get; set; } = "Production";

    public bool CaptureRequestBody { get; set; } = true;

    public bool CaptureResponseBody { get; set; } = true;

    public int MaxRequestBodyBytes { get; set; } = 64 * 1024;

    public int MaxResponseBodyBytes { get; set; } = 64 * 1024;

    public bool CaptureHeaders { get; set; } = true;

    public string[] SensitiveHeaders { get; set; } =
    [
        "Authorization",
        "Cookie",
        "Set-Cookie",
        "X-Api-Key",
        "Proxy-Authorization"
    ];

    public string[] ExcludedPaths { get; set; } =
    [
        "/health",
        "/metrics",
        "/favicon.ico"
    ];

    public int ChannelCapacity { get; set; } = 100_000;

    public int BatchSize { get; set; } = 500;

    public int FlushIntervalMilliseconds { get; set; } = 1000;

    public bool DropWhenChannelFull { get; set; } = true;

    public bool LogRepositoryErrors { get; set; } = true;
}
