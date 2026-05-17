using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace TopinLite.Biz.ChargeSaleHandler.Orders
{
    public sealed class OrderStoreOptions
    {
        public const string SectionName = "ChargeOrderStore";

        /// <summary>Counter key used by INCR to mint new OrderIds.</summary>
        public string OrderIdSequenceKey { get; set; } = "topup:orderid:seq";

        /// <summary>Format string for the per-order context key. {0} = OrderId.</summary>
        public string OrderContextKeyFormat { get; set; } = "topup:order:{0}";

        /// <summary>How long an unconfirmed order survives in Redis.</summary>
        public int ContextTtlSeconds { get; set; } = 900; // 15 minutes
    }

    public interface IOrderStore
    {
        Task<decimal> NextOrderIdAsync(CancellationToken cancellationToken);
        Task SaveAsync(OrderContext context, CancellationToken cancellationToken);
        Task<OrderContext?> TryGetAsync(decimal orderId, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(decimal orderId, CancellationToken cancellationToken);
    }

    public sealed class RedisOrderStore : IOrderStore
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = null
        };

        private readonly IConnectionMultiplexer _redis;
        private readonly OrderStoreOptions _options;

        public RedisOrderStore(IConnectionMultiplexer redis, IOptions<OrderStoreOptions> options)
        {
            _redis = redis;
            _options = options.Value;
        }

        public async Task<decimal> NextOrderIdAsync(CancellationToken cancellationToken)
        {
            // INCR is atomic across replicas — safe even with multiple ChargeSaleHandler
            // instances running. Redis returns Long; OrderId on the wire is decimal,
            // so we widen.
            IDatabase db = _redis.GetDatabase();
            long next = await db.StringIncrementAsync(_options.OrderIdSequenceKey).ConfigureAwait(false);
            return next;
        }

        public async Task SaveAsync(OrderContext context, CancellationToken cancellationToken)
        {
            IDatabase db = _redis.GetDatabase();
            string key = string.Format(_options.OrderContextKeyFormat, context.PkSeqPinlessCharge);
            string json = JsonSerializer.Serialize(context, JsonOptions);

            await db.StringSetAsync(
                key: key,
                value: json,
                expiry: TimeSpan.FromSeconds(_options.ContextTtlSeconds)).ConfigureAwait(false);
        }

        public async Task<OrderContext?> TryGetAsync(decimal orderId, CancellationToken cancellationToken)
        {
            IDatabase db = _redis.GetDatabase();
            string key = string.Format(_options.OrderContextKeyFormat, orderId);

            RedisValue raw = await db.StringGetAsync(key).ConfigureAwait(false);
            if (!raw.HasValue)
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<OrderContext>(raw.ToString(), JsonOptions);
            }
            catch
            {
                // corrupt context — treat as missing so caller surfaces "not found"
                return null;
            }
        }

        public async Task<bool> RemoveAsync(decimal orderId, CancellationToken cancellationToken)
        {
            IDatabase db = _redis.GetDatabase();
            string key = string.Format(_options.OrderContextKeyFormat, orderId);
            return await db.KeyDeleteAsync(key).ConfigureAwait(false);
        }
    }
}