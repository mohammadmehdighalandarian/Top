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
            IConfigurationBuilder cbuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", false, false);
            IConfigurationRoot configs = cbuilder.Build();

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
                                 rpc.AddHandler<BrokersAccessHandler, BrokersAccessRequestModel, BrokersAccessResponseModel>(subject: "infra.cache.broker-access", maxConcurrency: 128);
                                 rpc.AddHandler<DynamicConditionHandler, DynamicConditionsRequestModel, DynamicConditionsResponseModel>(subject: "infra.cache.dynamic-conditions", maxConcurrency: 128);
                                 rpc.AddHandler<BrokerOfferAccessHandler, BrokerOfferAccessRequestModel, BrokerOfferAccessResponseModel>(subject: "infra.cache.broker-offer-access", maxConcurrency: 128);
                                 rpc.AddHandler<DiyDataAccessHandler, DiyDataAccessRequestModel, int>(subject: "infra.cache.diy-data-access", maxConcurrency: 128);
                                 rpc.AddHandler<DiyPriceHandler, DiyPriceRequestModel, decimal>(subject: "infra.cache.diy-price", maxConcurrency: 128);
                                 rpc.AddHandler<PrimaryOfferHandler, PrimaryOfferRequestModel, List<PrimaryOfferResponseModel>>(subject: "infra.cache.primary-offers", maxConcurrency: 128);
                                 rpc.AddHandler<BrokerSaleLimitHandler, BrokerSaleLimitRequestModel, BrokerSaleLimitResponseModel>(subject: "infra.cache.broker-sale-limit", maxConcurrency: 128);
                                 rpc.AddHandler<TradeTypeHandler, TradeTypeRequestModel, TradeTypeResponseModel>(subject: "infra.cache.trade-types", maxConcurrency: 128);
                                 rpc.AddHandler<SelectedBrokerHandler, SelectedBrokerRequestModel, SelectedBrokerResponseModel>(subject: "infra.cache.selected-brokers", maxConcurrency: 128);
                                 rpc.AddHandler<BrokerSmsTextHandler, BrokerSmsRequestModel, BrokerSmsResponseModel>(subject: "infra.cache.broker-sms-text", maxConcurrency: 128);
                                 rpc.AddHandler<MessagesHandler, MessagesRequestModel, MessagesResponseModel>(subject: "infra.cache.messages", maxConcurrency: 128);
                             });

            return services;
        }
    }
}