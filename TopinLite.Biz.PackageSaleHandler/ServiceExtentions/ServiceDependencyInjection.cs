
using StackExchange.Redis;
using TopinLite.Domain.Configuration;

namespace TopinLite.Biz.PackageSaleHandler.ServiceExtentions;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfigurations(configuration);
        services.AddDependentServices(configuration);

        return services;
    }

    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationBuilder Cbuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", false, false);
        IConfigurationRoot Configs = Cbuilder.Build();

        return services;
    }

    public static IServiceCollection AddDependentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PackageOrderStoreOptions>(configuration.GetSection(PackageOrderStoreOptions.SectionName));

        RedisConfig.Host = configuration.GetValue<string>("RedisConfig:Host");
        RedisConfig.Port = configuration.GetValue<int>("RedisConfig:Port");

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            string cs = $"{RedisConfig.Host}:{RedisConfig.Port}";
            return ConnectionMultiplexer.Connect(cs);
        });

        services.AddSingleton<IProviderIdGenerator, ProviderIdGenerator>();
        services.AddSingleton<ICommonValidation, CommonValidation>();
        services.AddSingleton<IPackageOrderStore, RedisPackageOrderStore>();
        services.AddSingleton<IPackageServiceProvider, PackageServiceProvider>();


        services.AddNatsRpcFoundation(
                         natsUrl: $"nats://{configuration["Nats:HostAddress"]}:{configuration["Nats:Port"]}",
                         configure: rpc =>
                         {
                             rpc.AddHandler<RequestOrderPackageHandler, RequestOrderRequestMessageModel, RequestOrderResponseMessageModel>(subject: "biz.packagesale", maxConcurrency: 128);
                             rpc.AddHandler<ConfirmOrderPackageHandler, PackageConfirmOrderRequest, PackageConfirmOrderResponse>(subject: "biz.packagesale", maxConcurrency: 128);
                         });

        return services;
    }

}