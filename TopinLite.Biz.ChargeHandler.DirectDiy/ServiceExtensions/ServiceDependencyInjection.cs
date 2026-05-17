using NatsRpcFoundation.Extensions;
using TopinLite.Biz.ChargeHandler.DirectDiy.MessagingHandlers;
using TopinLite.Biz.ChargeHandler.DirectDiy.Validation;
using TopinLite.Domain.Messaging;

namespace TopinLite.Biz.ChargeHandler.DirectDiy.ServiceExtensions;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IDirectChargeValidationService, DirectChargeValidationService>();

        services.AddHttpClient("ESB", (_, client) =>
        {
            string? baseAddress = configuration.GetValue<string>("HuaweiCRM:HuaweiCRMBaseUri");
            if (Uri.TryCreate(baseAddress, UriKind.Absolute, out Uri? uri))
            {
                client.BaseAddress = uri;
            }
        });
        
        services.AddNatsRpcFoundation(configuration["Nats:HostAddress"]);

        services.AddNatsRpcFoundation(
            natsUrl: $"nats://{configuration["Nats:HostAddress"]}:{configuration["Nats:Port"]}",
            configure: rpc =>
            {
                rpc.AddHandler<DirectValidateHandler, ChargeTypeValidateRequest, ChargeTypeValidateResponse>(
                    subject: "biz.charge.directdiy.validate",
                    maxConcurrency: 128);
            });

        return services;
    }
}