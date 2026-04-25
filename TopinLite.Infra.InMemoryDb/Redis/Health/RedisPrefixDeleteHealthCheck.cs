using Microsoft.Extensions.Diagnostics.HealthChecks;

using StackExchange.Redis;

namespace TopinLite.Infra.InMemoryDb.Redis.Health
{
    public sealed class RedisPrefixDeleteHealthCheck : IHealthCheck
    {
        private readonly IConnectionMultiplexer _mux;

        public RedisPrefixDeleteHealthCheck(IConnectionMultiplexer mux)
        {
            _mux = mux;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _mux.GetDatabase();
                var pong = await db.PingAsync();

                return HealthCheckResult.Healthy($"Redis reachable. Ping={pong.TotalMilliseconds} ms");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Redis is not reachable.", ex);
            }
        }
    }
}
