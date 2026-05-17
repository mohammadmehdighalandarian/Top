using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Npgsql;

using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Configuration;
using TopinLite.infra.PostgreSQL.Internal;
using TopinLite.infra.PostgreSQL.Persistence;
using TopinLite.infra.PostgreSQL.Services;

namespace TopinLite.infra.PostgreSQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresHttpTrafficLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PostgresTrafficOptions>(
            configuration.GetSection(PostgresTrafficOptions.SectionName));

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<PostgresTrafficOptions>>().Value;
            if (string.IsNullOrWhiteSpace(options.ConnectionString))
            {
                throw new InvalidOperationException($"{PostgresTrafficOptions.SectionName}:ConnectionString is missing.");
            }

            var builder = new NpgsqlDataSourceBuilder(options.ConnectionString);
            return builder.Build();
        });

        services.AddSingleton<IHttpTrafficRepository, PostgresHttpTrafficRepository>();
        services.AddSingleton<ITopupSalesRepository, PostgresTopupSalesRepository>();
        services.AddSingleton<IHttpTrafficLogQueue, HttpTrafficLogQueue>();
        services.AddHostedService<HttpTrafficBackgroundWriter>();

        return services;
    }

    public static IApplicationBuilder UsePostgresHttpTrafficLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<Middleware.HttpTrafficLoggingMiddleware>();
    }
}
