global using NatsRpcFoundation.Extensions;

using TopinLite.Biz.PackageSaleHandler.MessagingHandlers;
using TopinLite.Domain.Messaging;
namespace TopinLite.Biz.PackageSaleHandler.ServiceExtentions
{
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
           
            services.AddSingleton<ServiceProviders.IPackageServiceProvider, ServiceProviders.PackageServiceProvider>();

            services.AddNatsRpcFoundation(
                             natsUrl: $"nats://{configuration["Nats:HostAddress"]}:{configuration["Nats:Port"]}",
                             configure: rpc =>
                             {
                                 rpc.AddHandler<RequestOrderPackageHandler, RequestOrderRequestMessageModel, RequestOrderResponseMessageModel>(subject: "biz.packagesale", maxConcurrency: 128);
                                
                             });

            return services;
        }
    }
}