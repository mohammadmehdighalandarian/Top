using System.Collections.Concurrent;
using System.Globalization;

namespace TopinLite.Services.Orders
{
    public sealed class PendingOrderContext
    {
        public decimal OrderId { get; init; }
        public string PayloadId { get; init; } = string.Empty;
        public decimal TelNum { get; init; }
        public decimal TelGift { get; init; }
        public string Amount { get; init; } = string.Empty;
        public string ChannelId { get; init; } = string.Empty;
        public string BrokerId { get; init; } = string.Empty;
        public decimal CustomerId { get; init; }
        public decimal VendorId { get; init; }
    }

    public interface IPendingOrderStore
    {
        void Upsert(PendingOrderContext context);
        bool TryGet(decimal orderId, out PendingOrderContext context);
        bool TryRemove(decimal orderId);
    }

    public sealed class InMemoryPendingOrderStore : IPendingOrderStore
    {
        private readonly ConcurrentDictionary<decimal, PendingOrderContext> _items = new();

        public void Upsert(PendingOrderContext context)
        {
            _items[context.OrderId] = context;
        }

        public bool TryGet(decimal orderId, out PendingOrderContext context)
        {
            return _items.TryGetValue(orderId, out context!);
        }

        public bool TryRemove(decimal orderId)
        {
            return _items.TryRemove(orderId, out _);
        }

        public static string NormalizeMsisdn(decimal value)
        {
            return decimal.Truncate(value).ToString("0", CultureInfo.InvariantCulture);
        }
    }
}
