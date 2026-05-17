using TopinLite.Api.Edge.Middleware;
using TopinLite.infra.PostgreSQL;

namespace TopinLite.Api.Edge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            IConfigurationBuilder Cbuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

            IConfigurationRoot Configs = Cbuilder.Build();
            builder.Services.AddBasicAuthMiddleware<RedisBasicAuthValidator>(builder.Configuration);

            ServiceDependencyInjection.AddDependency(builder.Services, Configs);
            builder.Services.AddPostgresHttpTrafficLogging(builder.Configuration);
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();
            builder.WebHost.ConfigureKestrel(serverOptions => serverOptions.AddServerHeader = false);

            var app = builder.Build();
            app.UseHealthChecks("/health");
            app.Urls.Add("http://*:3300");

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UsePostgresHttpTrafficLogging();
            app.AddUserActivityLogMiddleware();
            app.UseAuthorization();
            app.UseBasicAuthMiddleware();
            app.UseRouting();
            app.UseMiddleware<ExceptionLoggerMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}