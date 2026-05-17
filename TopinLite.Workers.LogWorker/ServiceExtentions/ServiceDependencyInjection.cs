using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using TopinLite.Domain.Configuration;
using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Configuration;
using TopinLite.infra.PostgreSQL.Persistence;
using TopinLite.Workers.LogWorker.Configuration;
using TopinLite.Workers.LogWorker.Consumers;
using TopinLite.Workers.LogWorker.Persistence;

namespace TopinLite.Workers.LogWorker.ServiceExtentions;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bind options
        services.Configure<RabbitMqConfigModel>(configuration.GetSection("RabbitConfig"));
        services.Configure<LogConsumerOptions>(configuration.GetSection(LogConsumerOptions.SectionName));
        services.Configure<PostgresTrafficOptions>(configuration.GetSection(PostgresTrafficOptions.SectionName));

        // One Npgsql data source for the whole worker — connection pooling is
        // handled internally by Npgsql.
        services.AddSingleton<NpgsqlDataSource>(sp =>
        {
            PostgresTrafficOptions pgOptions = sp.GetRequiredService<IOptions<PostgresTrafficOptions>>().Value;
            if (string.IsNullOrWhiteSpace(pgOptions.ConnectionString))
            {
                throw new InvalidOperationException(
                    $"{PostgresTrafficOptions.SectionName}:ConnectionString is missing in appsettings.json");
            }

            NpgsqlDataSourceBuilder builder = new(pgOptions.ConnectionString);
            return builder.Build();
        });

        // Repositories — reuse the existing http-traffic one, add our own for
        // user-activity and error tables.
        services.AddSingleton<IHttpTrafficRepository, PostgresHttpTrafficRepository>();
        services.AddSingleton<ILogRepository, PostgresLogRepository>();

        // Hosted consumers — one per queue, each on its own connection/channel.
        services.AddHostedService<HttpCallLogConsumer>();
        services.AddHostedService<UserActivityLogConsumer>();
        services.AddHostedService<ErrorLogConsumer>();

        return services;
    }
}