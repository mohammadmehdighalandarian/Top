using Microsoft.AspNetCore.Mvc;
using TopinLite.CrmTransform.CheckOfferEligibility.ServiceExtentions;

namespace TopinLite.CrmTransform.CheckOfferEligibility
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:2017");

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

            app.MapPost("/CheckOfferEligibility", async (HttpContext httpContext, [FromServices] IMediator _mediator, [FromBody] CheckOfferEligibilityTcpRequest model) =>
                {
                    ExecResult<GeneralHuawiResponse> Result = await _mediator.Send(new CheckOfferEligibilityCommand(new CheckOfferEligibilityTcpRequest
                    {
                        Mss = model.Mss,
                        PrimaryIdentity = model.PrimaryIdentity,
                        OfferId = model.OfferId
                    }));

                    return Results.Json(Result);
                })
                .WithName("CheckOfferEligibility")
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
        }
    }
}