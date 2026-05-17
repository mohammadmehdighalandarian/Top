using TopinLite.Biz.ChargeHandler.Loyalty.ServiceExtensions;

namespace TopinLite.Biz.ChargeHandler.Loyalty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:4102");
 
            builder.Services.AddHealthChecks();
            builder.Services.AddApplicationDependencies(builder.Configuration);
 
            var app = builder.Build();
            app.UseHealthChecks("/health");
            app.MapGet("/", () => "TopinLite.Biz.ChargeHandler.Loyalty is running.");
            app.Run();
        }
    }
}
