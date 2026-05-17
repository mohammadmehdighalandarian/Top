using TopinLite.Biz.ChargeHandler.Women.ServiceExtensions;

namespace TopinLite.Biz.ChargeHandler.Women
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:4103");
 
            builder.Services.AddHealthChecks();
            builder.Services.AddApplicationDependencies(builder.Configuration);
 
            var app = builder.Build();
            app.UseHealthChecks("/health");
            app.MapGet("/", () => "TopinLite.Biz.ChargeHandler.Women is running.");
            app.Run();
        }
    }
}
