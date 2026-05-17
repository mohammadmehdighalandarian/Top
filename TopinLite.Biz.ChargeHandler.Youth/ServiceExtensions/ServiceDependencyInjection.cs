using NatsRpcFoundation.Extensions;
using TopinLite.Biz.ChargeHandler.Youth.MessagingHandlers;
using TopinLite.Biz.ChargeHandler.Youth.Validation;
using TopinLite.Domain.Messaging;

namespace TopinLite.Biz.ChargeHandler.Youth.ServiceExtensions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient("ESB", (_, client) =>
            {
                string? baseAddress = configuration.GetValue<string>("HuaweiCRM:HuaweiCRMBaseUri");
                if (Uri.TryCreate(baseAddress, UriKind.Absolute, out Uri? uri))
                {
                    client.BaseAddress = uri;
                }
            });

            services.AddSingleton<IYouthChargeValidationService, YouthChargeValidationService>();
            
            services.AddNatsRpcFoundation(configuration["Nats:HostAddress"]);
            
            services.AddNatsRpcFoundation(
                natsUrl: $"nats://{configuration["Nats:HostAddress"]}:{configuration["Nats:Port"]}",
                configure: rpc =>
                {
                    rpc.AddHandler<YouthValidateHandler, ChargeTypeValidateRequest, ChargeTypeValidateResponse>(
                        subject: "biz.charge.youth.validate",
                        maxConcurrency: 128);
                });

            return services;
        }
    }
}