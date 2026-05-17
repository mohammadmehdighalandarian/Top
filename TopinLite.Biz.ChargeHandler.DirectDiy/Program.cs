using TopinLite.Biz.ChargeHandler.DirectDiy.ServiceExtensions;

namespace TopinLite.Biz.ChargeHandler.DirectDiy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:4101");
 
            builder.Services.AddHealthChecks();
            builder.Services.AddApplicationDependencies(builder.Configuration);
 
            var app = builder.Build();
            app.UseHealthChecks("/health");
            app.MapGet("/", () => "TopinLite.Biz.ChargeHandler.Direct is running.");
            app.Run();

        }
    }
}
