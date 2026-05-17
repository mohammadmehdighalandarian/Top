namespace NatsRpcFoundation.Dispatching;

public sealed class RpcHandlerRegistration
{
    public string Subject { get; init; } = string.Empty;
    public Type HandlerType { get; init; } = default!;
    public Type RequestType { get; init; } = default!;
    public Type ResponseType { get; init; } = default!;
    public int MaxConcurrency { get; init; } = 64;
}
