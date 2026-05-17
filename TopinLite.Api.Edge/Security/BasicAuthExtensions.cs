namespace TopinLite.Api.Edge.Security
{
    public static class BasicAuthExtensions
    {
        public static IServiceCollection AddBasicAuthMiddleware<TValidator>(this IServiceCollection services, IConfiguration configuration)
            where TValidator : class, IBasicAuthUserValidator
        {
            services.Configure<BasicAuthMiddlewareOptions>(
                configuration.GetSection("BasicAuth"));

            services.AddScoped<IBasicAuthUserValidator, TValidator>();
            services.AddTransient<BasicAuthMiddleware>();

            return services;
        }

        public static IServiceCollection AddBasicAuthMiddleware<TValidator>(this IServiceCollection services, Action<BasicAuthMiddlewareOptions> configure)
            where TValidator : class, IBasicAuthUserValidator
        {
            services.Configure(configure);

            services.AddScoped<IBasicAuthUserValidator, TValidator>();
            services.AddTransient<BasicAuthMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<BasicAuthMiddleware>();
        }
    }
}