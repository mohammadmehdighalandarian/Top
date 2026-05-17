using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;

namespace NatsRpcFoundation.Dispatching;

internal static class RpcHandlerExecutorFactory
{
    public static RpcHandlerExecutor Create(RpcHandlerRegistration registration, IRpcSerializer serializer)
    {
        var handlerType = registration.HandlerType;
        var requestType = registration.RequestType;
        var responseType = registration.ResponseType;

        return new RpcHandlerExecutor
        {
            ResolveHandler = BuildResolver(handlerType),
            ExecuteAsync = BuildExecutor(handlerType, requestType, responseType, serializer)
        };
    }

    private static Func<IServiceProvider, object> BuildResolver(Type handlerType)
    {
        var serviceProviderParam = Expression.Parameter(typeof(IServiceProvider), "sp");
        var getRequiredServiceMethod = typeof(ServiceProviderServiceExtensions)
            .GetMethods()
            .Single(m => m.Name == nameof(ServiceProviderServiceExtensions.GetRequiredService)
                      && m.IsGenericMethod
                      && m.GetParameters().Length == 1)
            .MakeGenericMethod(handlerType);

        var call = Expression.Call(getRequiredServiceMethod, serviceProviderParam);
        var cast = Expression.Convert(call, typeof(object));
        return Expression.Lambda<Func<IServiceProvider, object>>(cast, serviceProviderParam).Compile();
    }

    private static Func<object, byte[], RpcContext, CancellationToken, ValueTask<byte[]>> BuildExecutor(
        Type handlerType,
        Type requestType,
        Type responseType,
        IRpcSerializer serializer)
    {
        var method = typeof(RpcHandlerExecutorFactory)
            .GetMethod(nameof(BuildTypedExecutor), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
            .MakeGenericMethod(handlerType, requestType, responseType);

        return (Func<object, byte[], RpcContext, CancellationToken, ValueTask<byte[]>>)method.Invoke(null, new object[] { serializer })!;
    }

    private static Func<object, byte[], RpcContext, CancellationToken, ValueTask<byte[]>> BuildTypedExecutor<THandler, TRequest, TResponse>(
        IRpcSerializer serializer)
        where THandler : class, IRpcHandler<TRequest, TResponse>
    {
        return async (handlerObj, body, context, ct) =>
        {
            var handler = (THandler)handlerObj;
            var envelope = serializer.Deserialize<RpcEnvelope<TRequest>>(body);

            var enrichedContext = new RpcContext
            {
                Subject = context.Subject,
                CorrelationId = envelope.CorrelationId,
                TraceId = envelope.TraceId,
                TenantId = envelope.TenantId,
                MessageType = envelope.MessageType,
                Headers = envelope.Headers,
                ReceivedAtUtc = context.ReceivedAtUtc
            };

            var result = await handler.HandleAsync(enrichedContext, envelope.Payload, ct);
            return serializer.Serialize(result);
        };
    }
}
