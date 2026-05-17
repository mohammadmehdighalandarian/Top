using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Dispatching;
using NatsRpcFoundation.Internal;

namespace NatsRpcFoundation.Registration;

public sealed class RpcRegistrationBuilder : IRpcRegistrationBuilder
{
    private readonly List<RpcHandlerRegistration> _registrations = [];

    public IReadOnlyList<RpcHandlerRegistration> Registrations => _registrations;

    public IRpcRegistrationBuilder AddHandler<THandler, TRequest, TResponse>(
        string subject,
        int maxConcurrency = 64)
        where THandler : class, IRpcHandler<TRequest, TResponse>
    {
        Guard.AgainstNullOrWhiteSpace(subject, nameof(subject));
        Guard.AgainstOutOfRange(maxConcurrency, nameof(maxConcurrency), 1);

        _registrations.Add(new RpcHandlerRegistration
        {
            Subject = subject,
            HandlerType = typeof(THandler),
            RequestType = typeof(TRequest),
            ResponseType = typeof(TResponse),
            MaxConcurrency = maxConcurrency
        });

        return this;
    }
}
