using TopinLite.Infra.Common.Services;
using TopinLite.Services.Commons;

namespace TopinLite.Api.Edge.ServiceExtentions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddDependency(IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzkyNjI3MjAwIiwiaWF0IjoiMTc2MTEyOTI1NyIsImFjY291bnRfaWQiOiIwMTlhMGI3YjI5YTg3ZWY2OTA1MjU5ZjI0NWNlYjE3YiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazg1cXFwNjl6aGd3ejg0NjQ1NWRramJqIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.e9IechMB6PJqY-6Mhb5PGTFsJurNfgktkyem5gAh6z_vf9zJnvPl_6jEF7xckd_2XMD90PqOoxonfLtO2oeaGWxiT-HN7PLqUYIM5lOF3lb_OUkMLvyfE71vezCpjg0tyWAKlrESKlIIRuVcKeVIJm79K7JVVJiW5jqKsD2YKcCfvqdZTpFOsH6NtmCoI5__0rgj-8-wcClOpnOXvw-Iubh3FWwuWdLFjgqAW-UfLDR92inISKacjZViLRriqS2lI5vTKYAv6YIDLIqpkn0rir9nXlEXchT0Bza9Ds7OkkqVV1DFVSb1kQBO3oOSUYQfCHDNLvAczsAhpmqau5wMaQ";
            });

            AddApiDestinations(services, configuration);
            AddOtherServices(services);
            AddCommands(services);

            return services;
        }

        public static IServiceCollection AddApiDestinations(IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        private static IServiceCollection AddOtherServices(IServiceCollection services)
        {
            services.AddSingleton<IProviderIdGenerator, ProviderIdGenerator>();
            services.AddNatsRpcFoundation("nats://172.18.63.44:4222");
            return services;
        }

        private static IServiceCollection AddCommands(IServiceCollection services)
        {
            services.AddSingleton<IRequestHandler<CallSaleProviderChargeCommand, ExecResult<ExecResult>>, CallSaleProviderChargeCommandHandler>();
            services.AddSingleton<IRequestHandler<ConfirmOrderCommand, ExecResult<ConfirmOrderResponseModel>>, ConfirmOrderCommandHandler>();
            services.AddSingleton<IRequestHandler<RequestOrderCommand, ExecResult<RequestOrderResponseModel>>, RequestOrderCommandHandler>();

            return services;
        }
    }
}