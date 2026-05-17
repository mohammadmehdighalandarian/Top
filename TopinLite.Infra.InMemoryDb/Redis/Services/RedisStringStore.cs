using Microsoft.Extensions.Options;

using StackExchange.Redis;

using TopinLite.Infra.InMemoryDb.Redis.Abstractions;
using TopinLite.Infra.InMemoryDb.Redis.Configuration;

using Microsoft.Extensions.Logging;

namespace TopinLite.Infra.InMemoryDb.Redis.Services
{
    public sealed class RedisStringStore : IRedisStringStore
    {
        private readonly IConnectionMultiplexer _mux;
        private readonly RedisStringStoreOptions _options;
        private readonly ILogger<RedisStringStore> _logger;

        public RedisStringStore(
            IConnectionMultiplexer mux,
            IOptions<RedisStringStoreOptions> options,
            ILogger<RedisStringStore> logger)
        {
            _mux = mux ?? throw new ArgumentNullException(nameof(mux));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private IDatabase Db => _mux.GetDatabase(_options.DefaultDatabase);

        private RedisKey BuildKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Redis key can not be null or empty.", nameof(key));

            return string.IsNullOrWhiteSpace(_options.KeyPrefix)
                ? key
                : $"{_options.KeyPrefix}{key}";
        }

        public async ValueTask<bool> SetAsync(
            string key,
            string value,
            TimeSpan? expiry = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var redisKey = BuildKey(key);

            try
            {
                return await Db.StringSetAsync(
                    redisKey,
                    value,
                    expiry,
                    when: When.Always,
                    flags: CommandFlags.DemandMaster).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Redis SET failed for key: {Key}", redisKey);
                throw;
            }
        }

        public async ValueTask<string?> GetAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var redisKey = BuildKey(key);

            try
            {
                var value = await Db.StringGetAsync(
                    redisKey,
                    CommandFlags.PreferReplica).ConfigureAwait(false);

                return value.IsNull ? null : value.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Redis GET failed for key: {Key}", redisKey);
                throw;
            }
        }

        public async ValueTask<bool> DeleteAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var redisKey = BuildKey(key);

            try
            {
                return await Db.KeyDeleteAsync(
                    redisKey,
                    CommandFlags.DemandMaster).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Redis DELETE failed for key: {Key}", redisKey);
                throw;
            }
        }

        public async ValueTask<bool> ExistsAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var redisKey = BuildKey(key);

            try
            {
                return await Db.KeyExistsAsync(
                    redisKey,
                    CommandFlags.PreferReplica).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Redis EXISTS failed for key: {Key}", redisKey);
                throw;
            }
        }

        public async ValueTask<string?> GetOrSetAsync(
            string key,
            Func<CancellationToken, Task<string?>> factory,
            TimeSpan? expiry = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            var redisKey = BuildKey(key);

            try
            {
                var cached = await Db.StringGetAsync(
                    redisKey,
                    CommandFlags.PreferReplica).ConfigureAwait(false);

                if (!cached.IsNull)
                    return cached.ToString();

                var value = await factory(cancellationToken).ConfigureAwait(false);
                if (value is null)
                    return null;

                await Db.StringSetAsync(
                    redisKey,
                    value,
                    expiry,
                    when: When.NotExists,
                    flags: CommandFlags.DemandMaster).ConfigureAwait(false);

                var finalValue = await Db.StringGetAsync(
                    redisKey,
                    CommandFlags.PreferReplica).ConfigureAwait(false);

                return finalValue.IsNull ? value : finalValue.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Redis GETORSET failed for key: {Key}", redisKey);
                throw;
            }
        }
    }
}