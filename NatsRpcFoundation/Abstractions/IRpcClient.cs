using NatsRpcFoundation.Contracts;

namespace NatsRpcFoundation.Abstractions;

public interface IRpcClient
{
    Task<RpcResult<TResponse>> RequestAsync<TRequest, TResponse>(
        string subject,
        TRequest request,
        RpcCallOptions? options = null,
        CancellationToken cancellationToken = default);
}
