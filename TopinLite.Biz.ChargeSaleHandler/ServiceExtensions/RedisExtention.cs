namespace TopinLite.Biz.ChargeSaleHandler.ServiceExtensions
{
    public static class RedisExtention
    {
        //public static IServiceCollection AddStackExchangeRedisExtensions<T>(this IServiceCollection services, Func<IServiceProvider, IEnumerable<StackExchange.Redis.Extensions.Core.Configuration.RedisConfiguration>> redisConfigurationFactory)
        //where T : class, StackExchange.Redis.Extensions.Core.ISerializer
        //{
        //    services.AddSingleton<StackExchange.Redis.Extensions.Core.Abstractions.IRedisClientFactory, StackExchange.Redis.Extensions.Core.Implementations.RedisClientFactory>();
        //    services.AddSingleton<StackExchange.Redis.Extensions.Core.ISerializer, T>();

        //    services.AddSingleton((provider) => provider
        //        .GetRequiredService<StackExchange.Redis.Extensions.Core.Abstractions.IRedisClientFactory>()
        //        .GetDefaultRedisClient());

        //    services.AddSingleton((provider) => provider
        //        .GetRequiredService<StackExchange.Redis.Extensions.Core.Abstractions.IRedisClientFactory>()
        //        .GetDefaultRedisClient()
        //        .GetDefaultDatabase());

        //    services.AddSingleton(redisConfigurationFactory);

        //    return services;
        //}
    }
}
