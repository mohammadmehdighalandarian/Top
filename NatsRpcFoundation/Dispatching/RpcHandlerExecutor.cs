using NatsRpcFoundation.Contracts;

namespace NatsRpcFoundation.Dispatching;

internal sealed class RpcHandlerExecutor
{
    public required Func<IServiceProvider, object> ResolveHandler { get; init; }
    public required Func<object, byte[], RpcContext, CancellationToken, ValueTask<byte[]>> ExecuteAsync { get; init; }
}
