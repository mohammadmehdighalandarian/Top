using Microsoft.AspNetCore.Mvc;
using TopinLite.CrmTransform.QuerySimType.ServiceExtentions;

namespace TopinLite.CrmTransform.QuerySimType
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:2014");

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

            app.MapPost("/QuerySimType", async (HttpContext httpContext, [FromServices] IMediator _mediator, [FromBody] QuerySimTypeTcpRequest model) =>
                {
                    ExecResult<GeneralHuawiResponse> Result = await _mediator.Send(new QuerySimTypeCommand(new QuerySimTypeTcpRequest
                    {
                        Mss = model.Mss,
                        SubscriberNo = model.SubscriberNo
                    }));

                    return Results.Json(Result);
                })
                .WithName("QuerySimType")
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