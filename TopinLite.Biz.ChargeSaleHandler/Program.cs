
using TopinLite.Biz.ChargeSaleHandler.ServiceExtensions;

namespace TopinLite.Biz.ChargeSaleHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:4011");
 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            builder.Services.AddHealthChecks();
            builder.Services.AddApplicationDependencies(builder.Configuration);
 
            var app = builder.Build();
            app.MapOpenApi();
            app.UseHealthChecks("/health");
            app.MapGet("/", () => "TopinLite.Biz.ChargeSaleHandler is running.");
 
            app.Run();

        }
    }
}
