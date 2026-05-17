namespace NatsRpcFoundation.Contracts;

public sealed class RpcEnvelope<T>
{
    public string CorrelationId { get; init; } = Guid.NewGuid().ToString("N");
    public string? TraceId { get; init; }
    public string? TenantId { get; init; }
    public string? MessageType { get; init; }
    public long CreatedAtUnixMs { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public Dictionary<string, string>? Headers { get; init; }
    public T Payload { get; init; } = default!;
}
