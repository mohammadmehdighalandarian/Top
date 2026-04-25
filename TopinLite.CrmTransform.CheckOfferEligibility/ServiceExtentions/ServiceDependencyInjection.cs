using NatsRpcFoundation.Extensions;

namespace TopinLite.CrmTransform.CheckOfferEligibility.ServiceExtentions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddConfigurations(configuration);
            services.AddRedis(configuration);
            services.AddApiDestinations(configuration);
            services.AddDependentServices(configuration);

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            HuaweiCRMCommon.HuaweiCRMBaseUri = configuration.GetSection("HuaweiCRM:HuaweiCRMBaseUri").Value;
            HuaweiCRMCommon.EnableRabbitLogger = configuration.GetSection("HuaweiCRM:EnableRabbitLogger").Value;
            HuaweiCRMCommon.EnableRabbitHttpClientLogger = configuration.GetSection("HuaweiCRM:EnableRabbitHttpClientLogger").Value;
            HuaweiCRMCommon.LoginSystemCode = configuration.GetSection("HuaweiCRM:LoginSystemCode").Value;
            HuaweiCRMCommon.Password = configuration.GetSection("HuaweiCRM:Password").Value;

            RedisConfig.AbortOnConnectFail = configuration.GetValue<bool>("RedisConfig:AbortOnConnectFail");
            RedisConfig.KeyPrefix = configuration.GetValue<string>("RedisConfig:KeyPrefix");
            RedisConfig.Host = configuration.GetValue<string>("RedisConfig:Host");
            RedisConfig.Port = configuration.GetValue<int>("RedisConfig:Port");
            RedisConfig.Password = configuration.GetValue<string>("RedisConfig:Password");
            RedisConfig.AllowAdmin = configuration.GetValue<bool>("RedisConfig:AllowAdmin");
            RedisConfig.ConnectTimeout = configuration.GetValue<int>("RedisConfig:ConnectTimeout");
            RedisConfig.Database = configuration.GetValue<int>("RedisConfig:Database");
            RedisConfig.PoolSize = configuration.GetValue<int>("RedisConfig:PoolSize");

            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            RedisConfiguration redisConfiguration = new()
            {
                AbortOnConnectFail = RedisConfig.AbortOnConnectFail,
                KeyPrefix = RedisConfig.KeyPrefix,
                Hosts = [new RedisHost { Host = RedisConfig.Host, Port = RedisConfig.Port }],
                AllowAdmin = RedisConfig.AllowAdmin,
                Password = RedisConfig.Password,
                ConnectTimeout = RedisConfig.ConnectTimeout,
                Database = RedisConfig.Database,
                PoolSize = RedisConfig.PoolSize,
                ConnectionSelectionStrategy = ConnectionSelectionStrategy.LeastLoaded,
                ServerEnumerationStrategy = new()
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
                }
            };

            var redisConfigurations = new[] { redisConfiguration };
            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(sp => redisConfigurations);

            return services;
        }

        public static IServiceCollection AddApiDestinations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Replace(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, CustomLoggingFilter>());
            services.AddHttpClient("ESB", c =>
            {
                c.BaseAddress = new Uri(HuaweiCRMCommon.HuaweiCRMBaseUri);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    MaxConnectionsPerServer = 50,
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2),
                    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
                    KeepAlivePingDelay = TimeSpan.FromSeconds(30),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(15),
                    KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always,
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        RemoteCertificateValidationCallback = (sender, cert, chain, errors) => true
                    }
                };
            })
            .UseHttpClientMetrics();

            return services;
        }

        public static IServiceCollection AddDependentServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<Infra.MsBroker.RabbitMQ.IRabbitMQBroker, Infra.MsBroker.RabbitMQ.Loggers.HttpClientLogger>();
            services.TryAddSingleton<Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint, Infra.ApiClient.SOAPApi.HuaweiEndpoint.Endpoint>();
            services.TryAddSingleton<Infra.ApiClient.SOAPApi.HuaweiEndpoint.ISoapClient, Infra.ApiClient.SOAPApi.HuaweiEndpoint.SoapClient>();

            services.AddNatsRpcFoundation(
                             natsUrl: $"nats://{configuration["Nats:HostAddress"]}:{configuration["Nats:Port"]}",
                             configure: rpc =>
                             {
                                 rpc.AddHandler<CheckOfferEligibilityCommand, CheckOfferEligibilityTcpRequest, GeneralHuawiResponse>(subject: "crm.checkoffereligibility", maxConcurrency: 128);
                             });

            return services;
        }
    }
}
