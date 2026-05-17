using TopinLite.Workers.LogWorker.ServiceExtentions;

namespace TopinLite.Workers.LogWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddApplicationDependencies(builder.Configuration);
 
            IHost host = builder.Build();
            host.Run();

        }
    }
}