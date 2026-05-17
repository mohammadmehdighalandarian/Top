using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using StackExchange.Redis;

using TopinLite.Infra.InMemoryDb.Redis.Abstractions;
using TopinLite.Infra.InMemoryDb.Redis.Health;
using TopinLite.Infra.InMemoryDb.Redis.Infrastructure;
using TopinLite.Infra.InMemoryDb.Redis.Services;

namespace TopinLite.Infra.InMemoryDb.Redis.Configuration
{
    public static class RedisServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisPrefixDeletion(
            this IServiceCollection services,
            IConfiguration configuration,
            string redisConnectionString)
        {
            services.Configure<RedisPrefixDeleteOptions>(
                configuration.GetSection(RedisPrefixDeleteOptions.SectionName));

            services.Configure<RedisStringStoreOptions>(
                configuration.GetSection(RedisStringStoreOptions.SectionName));

            services.TryAddSingleton<IConnectionMultiplexer>(_ =>
            {
                var options = ConfigurationOptions.Parse(redisConnectionString);
                options.AbortOnConnectFail = false;
                options.ConnectRetry = 3;
                options.ConnectTimeout = 5000;
                options.AsyncTimeout = 5000;
                options.SyncTimeout = 5000;

                return ConnectionMultiplexer.Connect(options);
            });

            services.AddSingleton<RedisPrefixDeleteMetrics>();
            services.AddSingleton<IRedisDistributedLock, RedisDistributedLock>();
            services.AddSingleton<IRedisPrefixDeleteService, RedisPrefixDeleteService>();
            services.TryAddSingleton<IRedisStringStore, RedisStringStore>();

            services.AddSingleton<RedisPrefixDeleteQueue>();
            services.AddSingleton<IRedisPrefixDeleteQueue>(sp => sp.GetRequiredService<RedisPrefixDeleteQueue>());
            services.AddHostedService<RedisPrefixDeleteWorker>();

            services.AddHealthChecks()
                .AddCheck<RedisPrefixDeleteHealthCheck>("redis_prefix_delete");

            return services;
        }

        public static IServiceCollection AddRedisStringStore(
            this IServiceCollection services,
            IConfiguration configuration,
            string redisConnectionString)
        {
            services.Configure<RedisStringStoreOptions>(
                configuration.GetSection(RedisStringStoreOptions.SectionName));

            services.TryAddSingleton<IConnectionMultiplexer>(_ =>
            {
                var options = ConfigurationOptions.Parse(redisConnectionString);
                options.AbortOnConnectFail = false;
                options.ConnectRetry = 3;
                options.ConnectTimeout = 5000;
                options.AsyncTimeout = 5000;
                options.SyncTimeout = 5000;

                return ConnectionMultiplexer.Connect(options);
            });

            services.TryAddSingleton<IRedisStringStore, RedisStringStore>();

            return services;
        }
    }
}
