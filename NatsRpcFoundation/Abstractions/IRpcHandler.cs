using NatsRpcFoundation.Contracts;

namespace NatsRpcFoundation.Abstractions;

public interface IRpcHandler<TRequest, TResponse>
{
    ValueTask<RpcResult<TResponse>> HandleAsync(
        RpcContext context,
        TRequest request,
        CancellationToken cancellationToken);
}
