using Microsoft.AspNetCore.Mvc;
using TopinLite.CrmTransform.RechargeByBroker.ServiceExtentions;

namespace TopinLite.CrmTransform.RechargeByBroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:2016");

            builder.Services.AddApplicationDependencies(builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddHealthChecks();

            var app = builder.Build();

            app.MapOpenApi();
            app.UseAuthorization();
            app.MapControllers();
            app.UseHealthChecks("/health");
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapPost("/RechargeByBroker", async (HttpContext httpContext, [FromServices] IMediator _mediator, [FromBody] RechargeByBrokerTcpRequest model) =>
                {
                    ExecResult<GeneralHuawiResponse> Result = await _mediator.Send(new RechargeByBrokerCommand(new RechargeByBrokerTcpRequest
                    {
                        Mss = model.Mss,
                        PrimaryIdentity = model.PrimaryIdentity,
                        Amount = model.Amount,
                        BeId = model.BeId,
                        BrokerId = model.BrokerId,
                        RechargeChannelID = model.RechargeChannelID,
                        TradeType = model.TradeType
                    }));

                    return Results.Json(Result);
                })
                .WithName("RechargeByBroker")
                .Produces<ExecResult<GeneralHuawiResponse>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            Counter counter = Metrics.CreateCounter($"{typeof(Program).Namespace.ToLower().Replace(".", "")}",
                $"Counts requests to the {typeof(Program).Namespace.ToLower().Replace(".", "")} API endpoints", new CounterConfiguration
                {
                    LabelNames = new[] { "method", "endpoint" }
                });
            app.Use((context, next) =>
            {
                counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
                return next();
            });
            app.UseHttpMetrics();
            app.UseMetricServer();

            app.Run();
        }
    }
}