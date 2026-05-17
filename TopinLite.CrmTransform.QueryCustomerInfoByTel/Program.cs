using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using TopinLite.CrmTransform.QueryCustomerInfoByTel.ServiceExtentions;
using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.TopinApi;
using TopinLite.Services.MiniApiCommands;

namespace TopinLite.CrmTransform.QueryCustomerInfoByTel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:2013");

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

            app.MapPost("/QueryCustomerByTel", async (HttpContext httpContext, [FromServices] IMediator _mediator, [FromBody] QueryCustomerInfoByTelTcpRequest model) =>
                {
                    ExecResult<GeneralHuawiResponse> Result = await _mediator.Send(new QueryCustomerInfoByTelCommand(new QueryCustomerInfoByTelTcpRequest
                    {
                        Mss = model.Mss,
                        PrimaryIdentity = model.PrimaryIdentity
                    }));

                    return Results.Json(Result);
                })
                .WithName("QueryCustomerByTel")
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