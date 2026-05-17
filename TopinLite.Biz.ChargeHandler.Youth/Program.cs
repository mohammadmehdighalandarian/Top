using TopinLite.Biz.ChargeHandler.Youth.ServiceExtensions;

namespace TopinLite.Biz.ChargeHandler.Youth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:4104");
 
            builder.Services.AddHealthChecks();
            builder.Services.AddApplicationDependencies(builder.Configuration);
 
            var app = builder.Build();
            app.UseHealthChecks("/health");
            app.MapGet("/", () => "TopinLite.Biz.ChargeHandler.Youth is running.");
            app.Run();
        }
    }
}
