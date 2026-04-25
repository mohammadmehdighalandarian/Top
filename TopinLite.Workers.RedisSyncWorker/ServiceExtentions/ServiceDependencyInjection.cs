using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopinLite.Infra.InMemoryDb.Redis.Configuration;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

using TopinLite.Domain.Configuration;
using TopinLite.infra.OracleDataAccess.Queries;

namespace TopinLite.Workers.RedisSyncWorker.ServiceExtentions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigurations(configuration);
            services.AddOtherServices();

            services.AddCommands();
            services.AddApiDestinations(configuration);

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationBuilder Cbuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", false, false);

            IConfigurationRoot Configs = Cbuilder.Build();

            services.Configure<OracleDatabaseConfigModel>(configuration.GetSection("OracleDatabaseConfig"));

            services.AddRedisPrefixDeletion(configuration, configuration["Redis:Configuration"]);
            services.AddRedisStringStore(configuration, redisConnectionString: configuration["Redis:Configuration"]);


            return services;
        }

        public static IServiceCollection AddApiDestinations(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddOtherServices(this IServiceCollection services)
        {
            services.AddSingleton<ITopinQueryRepository, TopinQueryRepository>();
            services.AddSingleton<SyncOperations.IOffers, SyncOperations.Offers>();
            services.AddSingleton<SyncOperations.IBrokers, SyncOperations.Brokers>();
            services.AddSingleton<SyncOperations.IBrokersAccessList, SyncOperations.BrokersAccessList>();
            services.AddSingleton<SyncOperations.IDynamicCondition, SyncOperations.DynamicCondition>();

            return services;
        }

        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services;
        }
    }
}