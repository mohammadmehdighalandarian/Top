namespace NatsRpcFoundation.Contracts;

public sealed class RpcContext
{
    public string Subject { get; init; } = string.Empty;
    public string CorrelationId { get; init; } = string.Empty;
    public string? TraceId { get; init; }
    public string? TenantId { get; init; }
    public string? MessageType { get; init; }
    public IReadOnlyDictionary<string, string>? Headers { get; init; }
    public DateTimeOffset ReceivedAtUtc { get; init; } = DateTimeOffset.UtcNow;
}
