using NatsRpcFoundation.Extensions;
using TopinLite.Biz.ChargeHandler.Loyalty.MessagingHandlers;
using TopinLite.Biz.ChargeHandler.Loyalty.Validation;
using TopinLite.Domain.Messaging;

namespace TopinLite.Biz.ChargeHandler.Loyalty.ServiceExtensions
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
            
            services.AddSingleton<ILoyaltyChargeValidationService, LoyaltyChargeValidationService>();
            
            services.AddNatsRpcFoundation(configuration["Nats:HostAddress"]);

            services.AddNatsRpcFoundation(
                natsUrl: $"nats://{configuration["Nats:HostAddress"]}:{configuration["Nats:Port"]}",
                configure: rpc =>
                {
                    rpc.AddHandler<LoyaltyValidateHandler, ChargeTypeValidateRequest, ChargeTypeValidateResponse>(
                        subject: "biz.charge.loyalty.validate",
                        maxConcurrency: 128);
                });

            return services;
        }
    }
}
