namespace NatsRpcFoundation.Abstractions;

public interface IRpcRegistrationBuilder
{
    IRpcRegistrationBuilder AddHandler<THandler, TRequest, TResponse>(
        string subject,
        int maxConcurrency = 64)
        where THandler : class, IRpcHandler<TRequest, TResponse>;
}
