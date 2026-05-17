using StackExchange.Redis;

using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Infra.InMemoryDb.Redis.Infrastructure
{
    public sealed class RedisDistributedLock : IRedisDistributedLock
    {
        private readonly IConnectionMultiplexer _mux;

        public RedisDistributedLock(IConnectionMultiplexer mux)
        {
            _mux = mux ?? throw new ArgumentNullException(nameof(mux));
        }

        public async Task<IAsyncDisposable?> TryAcquireAsync(
            string key,
            TimeSpan expiry,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var db = _mux.GetDatabase();
            var token = Guid.NewGuid().ToString("N");

            var acquired = await db.StringSetAsync(
                key,
                token,
                expiry,
                when: When.NotExists).ConfigureAwait(false);

            if (!acquired)
                return null;

            return new RedisLockHandle(db, key, token);
        }

        private sealed class RedisLockHandle : IAsyncDisposable
        {
            private static readonly LuaScript ReleaseScript = LuaScript.Prepare(@"
if redis.call('GET', @key) == @token then
    return redis.call('DEL', @key)
else
    return 0
end");

            private readonly IDatabase _db;
            private readonly RedisKey _key;
            private readonly RedisValue _token;
            private bool _disposed;

            public RedisLockHandle(IDatabase db, RedisKey key, RedisValue token)
            {
                _db = db;
                _key = key;
                _token = token;
            }

            public async ValueTask DisposeAsync()
            {
                if (_disposed) return;
                _disposed = true;

                try
                {
                    await _db.ScriptEvaluateAsync(
                        ReleaseScript,
                        new { key = _key, token = _token }).ConfigureAwait(false);
                }
                catch
                {
                    // intentionally swallowed
                }
            }
        }
    }
}
