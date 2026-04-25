namespace TopinLite.Domain.Configuration
{
    public static class HuaweiCRMCommon
    {
        public static string EnableRabbitLogger { get; set; }
        public static string HuaweiCRMBaseUri { get; set; }
        public static string HuaweiCRMBaseUri_REST { get; set; }
        public static string LoginSystemCode { get; set; }
        public static string Password { get; set; }
        public static string EnableRabbitHttpClientLogger { get; set;}
    }

    public static class OrdsConfig
    {
        public static string BaseUri { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
    }


    public static class RedisConfig
    {
        public static bool AbortOnConnectFail { get; set; }
        public static string KeyPrefix { get; set; }
        public static string Host { get; set; }
        public static int Port { get; set; }
        public static string Password { get; set; } 
        public static bool AllowAdmin { get; set; }
        public static int ConnectTimeout { get; set; }
        public static int Database { get; set; }
        public static int PoolSize { get; set; }
    }


    public static class PrometheusConfig
    {
        public static bool EnablePushGateway { get; set; }
        public static string PushGatewayURL { get; set; }
    }

    public static class HuaweiCRMRabbitMQ
    {
        public static string HostAddress { get; set; }
        public static string Port { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
    }
}