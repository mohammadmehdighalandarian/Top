namespace TopinLite.Infrastructure.CacheData.ServiceExtentions
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
            services.AddRedisStringStore(configuration, redisConnectionString: configuration["Redis:Configuration"]);
            services.AddSingleton<ServiceProviders.IInMemoryDataProvider, ServiceProviders.InMemoryDataProvider>();

            services.AddNatsRpcFoundation(
                             natsUrl: $"nats://{configuration["Nats:HostAddress"]}:{configuration["Nats:Port"]}",
                             configure: rpc =>
                             {
                                 rpc.AddHandler<OfferInfoHandler, OfferRequestModel, OfferResponseModel>(subject: "infra.cache.offers", maxConcurrency: 128);
                                 rpc.AddHandler<BrokerInfoHandler, BrokersRequestModel, BrokersResponseModel>(subject: "infra.cache.brokers", maxConcurrency: 128);
                                 rpc.AddHandler<BrokersAccessHandler, BrokersAccessRequestModel, BrokersAccessResponseModel>(subject: "infra.cache.broker-acccess", maxConcurrency: 128);
                             });

            return services;
        }
    }
}