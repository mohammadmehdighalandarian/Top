using TopinLite.CrmTransform.IntegrationEnquery.ServiceExtentions;

namespace TopinLite.CrmTransform.IntegrationEnquery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:2011");

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

            var pro = typeof(Program);
            var s = pro.Namespace;

            Counter counter = Metrics.CreateCounter($"{typeof(Program).Namespace.ToLower().Replace(".", "")}", $"Counts requests to the {typeof(Program).Namespace.ToLower().Replace(".", "")} API endpoints", new CounterConfiguration
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
