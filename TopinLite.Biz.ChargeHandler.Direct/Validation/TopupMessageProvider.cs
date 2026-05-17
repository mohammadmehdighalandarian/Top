using System.Globalization;

using Microsoft.Extensions.Options;

using StackExchange.Redis;

namespace TopinLite.Biz.ChargeHandler.Direct.Validation
{
    public sealed class RedisMessagesOptions
    {
        public string ConnectionString { get; set; } = "localhost:6379,abortConnect=false";
        public string KeyPrefix { get; set; } = "topup:error:";
        public string HashKey { get; set; } = "topup:error:messages";
    }

    public interface ITopupMessageProvider
    {
        Task<string> ResolveAsync(decimal code, string fallbackMessage, CancellationToken cancellationToken);
    }

    public sealed class RedisTopupMessageProvider : ITopupMessageProvider
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly RedisMessagesOptions _options;
        private readonly ILogger<RedisTopupMessageProvider> _logger;

        public RedisTopupMessageProvider(
            IConnectionMultiplexer connection,
            IOptions<RedisMessagesOptions> options,
            ILogger<RedisTopupMessageProvider> logger)
        {
            _connection = connection;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<string> ResolveAsync(decimal code, string fallbackMessage, CancellationToken cancellationToken)
        {
            try
            {
                IDatabase redis = _connection.GetDatabase();
                string field = code.ToString(CultureInfo.InvariantCulture);

                RedisValue byString = await redis.StringGetAsync($"{_options.KeyPrefix}{field}").ConfigureAwait(false);
                if (byString.HasValue)
                {
                    return byString.ToString();
                }

                RedisValue byHash = await redis.HashGetAsync(_options.HashKey, field).ConfigureAwait(false);
                if (byHash.HasValue)
                {
                    return byHash.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to resolve Redis message for code {Code}", code);
            }

            return fallbackMessage;
        }
    }
}
