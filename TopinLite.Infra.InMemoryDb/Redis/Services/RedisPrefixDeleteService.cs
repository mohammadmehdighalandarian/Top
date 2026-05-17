using Microsoft.Extensions.Options;

using StackExchange.Redis;

using System.Diagnostics;
using System.Net;

using TopinLite.Infra.InMemoryDb.Redis.Abstractions;
using TopinLite.Infra.InMemoryDb.Redis.Configuration;
using TopinLite.Infra.InMemoryDb.Redis.Infrastructure;

using Microsoft.Extensions.Logging;

namespace TopinLite.Infra.InMemoryDb.Redis.Services
{
    public sealed class RedisPrefixDeleteService : IRedisPrefixDeleteService
    {
        private readonly IConnectionMultiplexer _mux;
        private readonly IRedisDistributedLock _lock;
        private readonly RedisPrefixDeleteOptions _options;
        private readonly RedisPrefixDeleteMetrics _metrics;
        private readonly ILogger<RedisPrefixDeleteService> _logger;

        public RedisPrefixDeleteService(
            IConnectionMultiplexer mux,
            IRedisDistributedLock @lock,
            IOptions<RedisPrefixDeleteOptions> options,
            RedisPrefixDeleteMetrics metrics,
            ILogger<RedisPrefixDeleteService> logger)
        {
            _mux = mux;
            _lock = @lock;
            _options = options.Value;
            _metrics = metrics;
            _logger = logger;
        }

        public async Task<long> DeleteByPrefixAsync(
            string prefix,
            CancellationToken cancellationToken = default)
        {
            ValidatePrefix(prefix);

            var lockKey = _options.LockKeyPrefix + prefix;
            await using var handle = await _lock.TryAcquireAsync(lockKey, _options.LockExpiry, cancellationToken);

            if (handle is null)
            {
                _logger.LogWarning("Redis prefix delete skipped because lock is already held. Prefix={Prefix}", prefix);
                return 0;
            }

            var endpoints = _mux.GetEndPoints();
            if (endpoints.Length == 0)
            {
                _logger.LogWarning("No Redis endpoints found.");
                return 0;
            }

            long totalDeleted = 0;

            foreach (var endpoint in endpoints)
            {
                cancellationToken.ThrowIfCancellationRequested();

                IServer server;
                try
                {
                    server = _mux.GetServer(endpoint);
                }
                catch (Exception ex)
                {
                    _metrics.Failures.Add(1,
                        new KeyValuePair<string, object?>("stage", "get_server"),
                        new KeyValuePair<string, object?>("endpoint", endpoint.ToString()));

                    _logger.LogWarning(ex, "Could not get server for endpoint {Endpoint}", endpoint);
                    continue;
                }

                if (!server.IsConnected || server.IsReplica)
                    continue;

                totalDeleted += await DeleteOnServerAsync(server, prefix, cancellationToken);
            }

            _logger.LogInformation(
                "Redis prefix deletion finished. Prefix={Prefix}, TotalDeleted={Deleted}",
                prefix, totalDeleted);

            return totalDeleted;
        }

        private async Task<long> DeleteOnServerAsync(
            IServer server,
            string prefix,
            CancellationToken cancellationToken)
        {
            var db = _mux.GetDatabase(_options.Database);
            var pattern = prefix + "*";

            var inFlight = new List<Task<long>>(_options.MaxParallelBatchesPerServer);
            var batch = new List<RedisKey>(_options.DeleteBatchSize);
            long deletedCount = 0;

            IEnumerable<RedisKey> keys = server.Keys(
                database: _options.Database,
                pattern: pattern,
                pageSize: _options.ScanPageSize);

            foreach (var key in keys)
            {
                cancellationToken.ThrowIfCancellationRequested();

                batch.Add(key);

                if (batch.Count < _options.DeleteBatchSize)
                    continue;

                inFlight.Add(DeleteBatchWithRetryAsync(
                    db,
                    batch.ToArray(),
                    server.EndPoint,
                    cancellationToken));

                batch.Clear();

                if (inFlight.Count >= _options.MaxParallelBatchesPerServer)
                {
                    var completed = await Task.WhenAny(inFlight);
                    inFlight.Remove(completed);
                    deletedCount += await completed;
                }
            }

            if (batch.Count > 0)
            {
                inFlight.Add(DeleteBatchWithRetryAsync(
                    db,
                    batch.ToArray(),
                    server.EndPoint,
                    cancellationToken));
            }

            if (inFlight.Count > 0)
            {
                var results = await Task.WhenAll(inFlight);
                deletedCount += results.Sum();
            }

            return deletedCount;
        }

        private async Task<long> DeleteBatchWithRetryAsync(
            IDatabase db,
            RedisKey[] keys,
            EndPoint endpoint,
            CancellationToken cancellationToken)
        {
            Exception? lastError = null;

            for (int attempt = 1; attempt <= _options.MaxRetries; attempt++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var sw = Stopwatch.StartNew();
                _metrics.InFlightBatches.Add(1);

                try
                {
                    var deleted = await DeleteBatchCoreAsync(db, keys, cancellationToken);

                    sw.Stop();
                    _metrics.BatchesProcessed.Add(1,
                        new KeyValuePair<string, object?>("endpoint", endpoint.ToString()));
                    _metrics.KeysDeleted.Add(deleted,
                        new KeyValuePair<string, object?>("endpoint", endpoint.ToString()));
                    _metrics.BatchDurationMs.Record(sw.Elapsed.TotalMilliseconds,
                        new KeyValuePair<string, object?>("endpoint", endpoint.ToString()));
                    _metrics.BatchSize.Record(keys.Length,
                        new KeyValuePair<string, object?>("endpoint", endpoint.ToString()));

                    return deleted;
                }
                catch (Exception ex) when (attempt < _options.MaxRetries && IsTransient(ex))
                {
                    lastError = ex;
                    _metrics.Retries.Add(1,
                        new KeyValuePair<string, object?>("endpoint", endpoint.ToString()));

                    await Task.Delay(GetRetryDelay(attempt), cancellationToken);
                }
                catch (Exception ex)
                {
                    lastError = ex;
                    break;
                }
                finally
                {
                    _metrics.InFlightBatches.Add(-1);
                }
            }

            _metrics.Failures.Add(1,
                new KeyValuePair<string, object?>("stage", "delete_batch"),
                new KeyValuePair<string, object?>("endpoint", endpoint.ToString()));

            _logger.LogError(lastError,
                "Redis batch deletion failed after retries. Endpoint={Endpoint}, BatchSize={BatchSize}",
                endpoint, keys.Length);

            throw lastError ?? new InvalidOperationException("Unknown Redis prefix deletion failure.");
        }

        private async Task<long> DeleteBatchCoreAsync(
            IDatabase db,
            RedisKey[] keys,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (keys.Length == 0)
                return 0;

            if (_options.PreferUnlink)
            {
                try
                {
                    object[] args = new object[keys.Length];
                    for (int i = 0; i < keys.Length; i++)
                        args[i] = keys[i];

                    var result = await db.ExecuteAsync("UNLINK", args);
                    if (!result.IsNull && long.TryParse(result.ToString(), out var unlinked))
                        return unlinked;

                    return keys.Length;
                }
                catch (RedisServerException)
                {
                    // fallback to DEL
                }
                catch (RedisCommandException)
                {
                    // fallback to DEL
                }
            }

            return await db.KeyDeleteAsync(keys);
        }

        private void ValidatePrefix(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("Prefix cannot be null or empty.", nameof(prefix));

            if (prefix.Length < _options.MinimumPrefixLength)
                throw new ArgumentException(
                    $"Prefix length must be at least {_options.MinimumPrefixLength}.",
                    nameof(prefix));

            if (prefix == "*" || prefix == ":" || prefix == "_")
                throw new ArgumentException("Unsafe prefix value.", nameof(prefix));
        }

        private bool IsTransient(Exception ex)
        {
            return ex is RedisTimeoutException
                || ex is RedisConnectionException
                || ex is TimeoutException
                || ex is RedisException;
        }

        private TimeSpan GetRetryDelay(int attempt) => attempt switch
        {
            1 => TimeSpan.FromMilliseconds(_options.RetryDelayMs1),
            2 => TimeSpan.FromMilliseconds(_options.RetryDelayMs2),
            _ => TimeSpan.FromMilliseconds(_options.RetryDelayMs3)
        };
    }
}