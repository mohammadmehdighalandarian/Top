namespace NatsRpcFoundation.Contracts;

public sealed class RpcCallOptions
{
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(5);
    public string? CorrelationId { get; init; }
    public string? TraceId { get; init; }
    public string? TenantId { get; init; }
    public Dictionary<string, string>? Headers { get; init; }
}
