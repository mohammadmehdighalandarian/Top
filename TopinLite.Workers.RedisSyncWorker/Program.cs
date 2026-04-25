using TopinLite.Workers.RedisSyncWorker.ServiceExtentions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopinLite.Infra.InMemoryDb.Redis.Configuration;

namespace TopinLite.Workers.RedisSyncWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            ServiceDependencyInjection.AddApplicationDependencies(builder.Services, builder.Configuration);
            builder.Services.AddHostedService<Worker>();
            var host = builder.Build();
            host.Run();
        }
    }
}