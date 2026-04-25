
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi;

 using NatsRpcFoundation.Extensions;

namespace TopinLite.Biz.ChargeSaleHandler.ServiceExtensions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = null;
            });
            AddConfigurations(services, configuration);
            //AddRedis(services, configuration);
            //services.AddMediatR(_ => _.RegisterServicesFromAssembly(typeof(Program).Assembly));
            //services.AddOtherServices();
            //services.AddCommands();
            //services.AddApiDestinations(configuration);

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationBuilder Cbuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", false, false);

            IConfigurationRoot Configs = Cbuilder.Build();

            services.AddNatsRpcFoundation(Configs["NATS:Url"]);
            //Domain.Configuration.HuaweiCRMCommon.HuaweiCRMBaseUri = Configs.GetSection("HuaweiCRM:HuaweiCRMBaseUri").Value;
            //Domain.Configuration.HuaweiCRMCommon.EnableRabbitLogger = Configs.GetSection("HuaweiCRM:EnableRabbitLogger").Value;
            //Domain.Configuration.HuaweiCRMCommon.EnableRabbitHttpClientLogger = Configs.GetSection("HuaweiCRM:EnableRabbitHttpClientLogger").Value;
            //Domain.Configuration.HuaweiCRMCommon.LoginSystemCode = Configs.GetSection("HuaweiCRM:LoginSystemCode").Value;
            //Domain.Configuration.HuaweiCRMCommon.Password = Configs.GetSection("HuaweiCRM:Password").Value;

            //Domain.Configuration.RedisConfig.AbortOnConnectFail = Configs.GetValue<bool>("RedisConfig:AbortOnConnectFail");
            //Domain.Configuration.RedisConfig.KeyPrefix = Configs.GetValue<string>("RedisConfig:KeyPrefix");
            //Domain.Configuration.RedisConfig.Host = Configs.GetValue<string>("RedisConfig:Host");
            //Domain.Configuration.RedisConfig.Port = Configs.GetValue<int>("RedisConfig:Port");
            //Domain.Configuration.RedisConfig.Password = Configs.GetValue<string>("RedisConfig:Password");
            //Domain.Configuration.RedisConfig.AllowAdmin = Configs.GetValue<bool>("RedisConfig:AllowAdmin");
            //Domain.Configuration.RedisConfig.ConnectTimeout = Configs.GetValue<int>("RedisConfig:ConnectTimeout");
            //Domain.Configuration.RedisConfig.Database = Configs.GetValue<int>("RedisConfig:Database");
            //Domain.Configuration.RedisConfig.PoolSize = Configs.GetValue<int>("RedisConfig:PoolSize");

            //Domain.Configuration.PrometheusConfig.EnablePushGateway = Configs.GetValue<bool>("PrometheusConfig:EnablePushGateway");
            //Domain.Configuration.PrometheusConfig.PushGatewayURL = Configs.GetValue<string>("PrometheusConfig:PushGatewayURL");


            return services;
        }

        //public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        //{
        //    StackExchange.Redis.Extensions.Core.Configuration.RedisConfiguration redisConfiguration = new StackExchange.Redis.Extensions.Core.Configuration.RedisConfiguration()
        //    {
        //        AbortOnConnectFail = Domain.Configuration.RedisConfig.AbortOnConnectFail,
        //        KeyPrefix = Domain.Configuration.RedisConfig.KeyPrefix,
        //        Hosts = [new StackExchange.Redis.Extensions.Core.Configuration.RedisHost { Host = Domain.Configuration.RedisConfig.Host, Port = Domain.Configuration.RedisConfig.Port }],
        //        AllowAdmin = Domain.Configuration.RedisConfig.AllowAdmin,
        //        Password = Domain.Configuration.RedisConfig.Password,
        //        ConnectTimeout = Domain.Configuration.RedisConfig.ConnectTimeout,
        //        Database = Domain.Configuration.RedisConfig.Database,
        //        PoolSize = Domain.Configuration.RedisConfig.PoolSize,
        //        ConnectionSelectionStrategy = StackExchange.Redis.Extensions.Core.Configuration.ConnectionSelectionStrategy.LeastLoaded,
        //        ServerEnumerationStrategy = new()
        //        {
        //            Mode = StackExchange.Redis.Extensions.Core.Configuration.ServerEnumerationStrategy.ModeOptions.All,
        //            TargetRole = StackExchange.Redis.Extensions.Core.Configuration.ServerEnumerationStrategy.TargetRoleOptions.Any,
        //            UnreachableServerAction = StackExchange.Redis.Extensions.Core.Configuration.ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
        //        }
        //    };


        //    var redisConfigurations = new[] { redisConfiguration };
        //    services.AddStackExchangeRedisExtensions<StackExchange.Redis.Extensions.System.Text.Json.SystemTextJsonSerializer>(sp => redisConfigurations);
        //    return services;
        //}

        //public static IServiceCollection AddApiDestinations(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.Replace(ServiceDescriptor.Singleton<Microsoft.Extensions.Http.IHttpMessageHandlerBuilderFilter, CustomLoggingFilter>());
        //    services.AddHttpClient("ESB", c =>
        //    {
        //        c.BaseAddress = new Uri(Domain.Configuration.HuaweiCRMCommon.HuaweiCRMBaseUri);
        //    })
        //    .ConfigurePrimaryHttpMessageHandler(() =>
        //    {
        //        return new SocketsHttpHandler
        //        {
        //            MaxConnectionsPerServer = 50,
        //            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
        //            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
        //            KeepAlivePingDelay = TimeSpan.FromSeconds(30),
        //            KeepAlivePingTimeout = TimeSpan.FromSeconds(15),
        //            KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always,
        //            SslOptions = new System.Net.Security.SslClientAuthenticationOptions
        //            {
        //                RemoteCertificateValidationCallback = (sender, cert, chain, errors) => true
        //            }
        //        };
        //    })
        //    .UseHttpClientMetrics();

        //    return services;
        //}

        //public static IServiceCollection AddOtherServices(this IServiceCollection services)
        //{
        //    services.AddAuthorization();
        //    services.AddEndpointsApiExplorer();
        //    services.AddHealthChecks();
        //    services.AddSwaggerGen(options =>
        //    {
        //        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        //    });

        //    services.AddSwaggerGenNewtonsoftSupport();
        //    services.TryAddSingleton<Infra.MsBroker.RabbitMQ.IRabbitMQBroker, Infra.MsBroker.RabbitMQ.Loggers.HttpClientLogger>();
        //    services.TryAddSingleton<Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint, Infra.ApiClient.SOAPApi.HuaweiEndpoint.Endpoint>();
        //    services.TryAddSingleton<Infra.ApiClient.SOAPApi.HuaweiEndpoint.ISoapClient, Infra.ApiClient.SOAPApi.HuaweiEndpoint.SoapClient >();
        //    services.TryAddSingleton<IProviderIdGenerator, ProviderIdGenerator>();
        //    return services;
        //}

        //public static IServiceCollection AddCommands(this IServiceCollection services)
        //{
        //    services.TryAddSingleton<IRequestHandler<RequestOrderCommand, ExecResult<RequestOrderResponseModel>>, RequestOrderCommandHandler>();
        //    services.TryAddSingleton<IRequestHandler<ConfirmOrderCommand, ExecResult<ConfirmOrderResponseModel>>, ConfirmOrderCommandHandler>();
        //    services.TryAddSingleton<IPendingOrderStore, InMemoryPendingOrderStore>();
        //    return services;
        //}
    }
}