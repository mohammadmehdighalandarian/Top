using Microsoft.Extensions.Options;

using NATS.Client.Core;
using NATS.Client.Serializers.Json;

using StackExchange.Redis;

using TopinLite.Biz.ChargeHandler.Loyalty.Validation;

namespace TopinLite.Biz.ChargeHandler.Loyalty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<RedisMessagesOptions>(builder.Configuration.GetSection("RedisMessages"));
            builder.Services.AddSingleton<ITopupMessageProvider, RedisTopupMessageProvider>();
            builder.Services.AddSingleton<ILoyaltyChargeValidationService, LoyaltyChargeValidationService>();
            builder.Services.AddSingleton<ILoyaltyConfirmOrderService, LoyaltyConfirmOrderService>();

            builder.Services.AddSingleton<Infra.ApiClient.SOAPApi.HuaweiEndpoint.ISoapClient, Infra.ApiClient.SOAPApi.HuaweiEndpoint.SoapClient>();
            builder.Services.AddSingleton<Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint, Infra.ApiClient.SOAPApi.HuaweiEndpoint.Endpoint>();

            builder.Services.AddHttpClient("ESB", (_, client) =>
            {
                string? baseAddress = builder.Configuration.GetValue<string>("HuaweiCRM:HuaweiCRMBaseUri");
                if (Uri.TryCreate(baseAddress, UriKind.Absolute, out Uri? uri))
                {
                    client.BaseAddress = uri;
                }
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
            {
                RedisMessagesOptions options = serviceProvider.GetRequiredService<IOptions<RedisMessagesOptions>>().Value;
                string connectionString = string.IsNullOrWhiteSpace(options.ConnectionString)
                    ? "localhost:6379,abortConnect=false"
                    : options.ConnectionString;

                return ConnectionMultiplexer.Connect(connectionString);
            });

            builder.Services.AddSingleton(_ =>
            {
                var opts = NatsOpts.Default with
                {
                    Url = "nats://localhost:4222",
                    Name = "Biz.ChargeHandler.Loyalty",
                    SerializerRegistry = NatsJsonSerializerRegistry.Default
                };

                return new NatsConnection(opts);
            });

            builder.Services.AddHostedService<Responder>();

            var app = builder.Build();
            app.MapGet("/Biz/ChargeHandler/Loyalty", () => "TopinLite.Biz.ChargeHandler.Loyalty is running.");
            app.Run();
        }
    }
}
