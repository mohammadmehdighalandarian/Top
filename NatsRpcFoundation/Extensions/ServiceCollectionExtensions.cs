using Microsoft.Extensions.DependencyInjection;
using NATS.Client.Core;
using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Dispatching;
using NatsRpcFoundation.Hosting;
using NatsRpcFoundation.JetStream;
using NatsRpcFoundation.Registration;
using NatsRpcFoundation.Serialization;
using NatsRpcFoundation.Services;

namespace NatsRpcFoundation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNatsRpcFoundation(
        this IServiceCollection services,
        string natsUrl,
        Action<IRpcRegistrationBuilder>? configure = null)
    {
        services.AddSingleton<INatsConnection>(sp =>
        {
            var opts = new NatsOpts
            {
                Url = natsUrl
            };

            return new NatsConnection(opts);
        });

        services.AddSingleton(sp => (NatsConnection)sp.GetRequiredService<INatsConnection>());
        services.AddSingleton<IRpcSerializer, SystemTextJsonRpcSerializer>();
        services.AddSingleton<IRpcClient, RpcClient>();
        services.AddSingleton<IJetStreamService, JetStreamService>();

        var builder = new RpcRegistrationBuilder();
        configure?.Invoke(builder);

        foreach (var registration in builder.Registrations)
        {
            services.AddSingleton(registration);
            services.AddScoped(registration.HandlerType);
        }

        if (builder.Registrations.Count > 0)
        {
            services.AddHostedService<RpcResponderBackgroundService>();
        }

        return services;
    }
}
