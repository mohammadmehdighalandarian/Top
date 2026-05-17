using MediatR;
using Microsoft.AspNetCore.Mvc;
using NATS.Client.Core;
using NATS.Client.Serializers.Json;
using Prometheus;
using TopinLite.Biz.ChargeHandler.ServiceExtentions;
using TopinLite.Domain.Configuration;
using TopinLite.Domain.TopinApi;
using TopinLite.Services.Commands;


using StackExchange.Redis;


namespace TopinLite.Biz.ChargeHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args
            });

            ServiceDependencyInjection.AddApplicationDependencies(builder.Services, builder.Configuration);

            var natsConfiguration = builder.Configuration.GetSection("NATS").Get<NatsConfiguration>()!;

            builder.Services.AddSingleton(_ =>
            {
                var opts = NatsOpts.Default with
                {
                    Url = natsConfiguration.Url,
                    Name = "Biz.ChargeHandler.Request",
                    SerializerRegistry = NatsJsonSerializerRegistry.Default
                };
                return new NatsConnection(opts);
            });
            builder.Services.AddHostedService<RequestResponder>();

            builder.Services.AddSingleton(_ =>
            {
                var opts = NatsOpts.Default with
                {
                    Url = natsConfiguration.Url,
                    Name = "Biz.ChargeHandler.Confirm",
                    SerializerRegistry = NatsJsonSerializerRegistry.Default
                };
                return new NatsConnection(opts);
            });
            builder.Services.AddHostedService<ConfirmResponder>();

            var app = builder.Build();
            app.UseSwagger(options => options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
            app.UseSwaggerUI();
            app.UseAuthorization();
            app.UseHealthChecks("/health");
            app.Urls.Add("http://*:3002");

            app.MapPost("/Biz/ChargeHandler", async (
                HttpContext httpContext,
                [FromBody] RequestOrderEnvelope request,
                [FromServices] IMediator mediator
            ) =>
            {
                var command = new RequestOrderCommand(request);

                var result = await mediator.Send(command);

                return Results.Json(result);
            })
            .WithName("ChargeHandler")
            .Produces<ExecResult<RequestOrderResponseModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            app.UseMetricServer();
            app.UseHttpMetrics();
            if (PrometheusConfig.EnablePushGateway)
            {
                MetricPusher pusher = new MetricPusher(new MetricPusherOptions { Endpoint = PrometheusConfig.PushGatewayURL, Job = "QuerySubscriber_job" });
                pusher.Start();
            }
            app.Run();
        }
    }
}
