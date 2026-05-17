using NatsRpcFoundation.Extensions;
using StackExchange.Redis;
using TopinLite.Biz.ChargeSaleHandler.Crm;
using TopinLite.Biz.ChargeSaleHandler.MessagingHandlers;
using TopinLite.Biz.ChargeSaleHandler.Orders;
using TopinLite.Biz.ChargeSaleHandler.ServiceProviders;
using TopinLite.Biz.ChargeSaleHandler.Validation;
using TopinLite.Domain.Configuration;
using TopinLite.Domain.Messaging;
using TopinLite.Infra.Common.Services;
using TopinLite.Services.Commons;

namespace TopinLite.Biz.ChargeSaleHandler.ServiceExtensions;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OrderStoreOptions>(configuration.GetSection(OrderStoreOptions.SectionName));
        services.Configure<ChargeTypeRules>(configuration.GetSection(ChargeTypeRules.SectionName));
        services.Configure<PardisSmsConfigModel>(configuration.GetSection("Pardis"));
        
        RedisConfig.AbortOnConnectFail = configuration.GetValue<bool>("RedisConfig:AbortOnConnectFail");
        RedisConfig.KeyPrefix = configuration.GetValue<string>("RedisConfig:KeyPrefix");
        RedisConfig.Host = configuration.GetValue<string>("RedisConfig:Host");
        RedisConfig.Port = configuration.GetValue<int>("RedisConfig:Port");
        RedisConfig.Password = configuration.GetValue<string>("RedisConfig:Password");
        RedisConfig.AllowAdmin = configuration.GetValue<bool>("RedisConfig:AllowAdmin");
        RedisConfig.ConnectTimeout = configuration.GetValue<int>("RedisConfig:ConnectTimeout");
        RedisConfig.Database = configuration.GetValue<int>("RedisConfig:Database");
        RedisConfig.PoolSize = configuration.GetValue<int>("RedisConfig:PoolSize");

        services.AddHttpClient("PardisToken", x => { x.BaseAddress = new Uri(configuration.GetValue<string>("Pardis:TokenAddress")!); });
        services.AddHttpClient("Pardis", x => { x.BaseAddress = new Uri(configuration.GetValue<string>("Pardis:ClientAddress")!); });

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            string cs = $"{RedisConfig.Host}:{RedisConfig.Port}";
            return ConnectionMultiplexer.Connect(cs);
        });

        services.AddSingleton<IOrderStore, RedisOrderStore>();
        services.AddSingleton<IChargeTypeResolver, ChargeTypeResolver>();
        services.AddSingleton<ICommonValidator, CommonValidator>();
        services.AddSingleton<ICrmRechargeCaller, CrmRechargeCaller>();
        services.AddSingleton<IPardisSmsProvider, PardisSmsProvider>();
        
        services.AddSingleton<IChargeServiceProvider, ChargeServiceProvider>();
        
        services.AddNatsRpcFoundation(configuration["Nats:HostAddress"]);
        
        services.AddNatsRpcFoundation(
            natsUrl: $"nats://{configuration["Nats:HostAddress"]}:{configuration["Nats:Port"]}",
            configure: rpc =>
            {
                rpc.AddHandler<RequestOrderChargeHandler, ChargeRequestOrderRequest, ChargeRequestOrderResponse>(
                    subject: "biz.chargesale.requestorder",
                    maxConcurrency: 128);

                rpc.AddHandler<ConfirmOrderChargeHandler, ChargeConfirmOrderRequest, ChargeConfirmOrderResponse>(
                    subject: "biz.chargesale.confirmorder",
                    maxConcurrency: 128);
            });

        return services;
    }
}