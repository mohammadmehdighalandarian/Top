using System.Diagnostics.Metrics;

namespace TopinLite.Infra.InMemoryDb.Redis.Infrastructure
{
    public sealed class RedisPrefixDeleteMetrics
    {
        public const string MeterName = "YourApp.Redis.PrefixDelete";

        private readonly Meter _meter;

        public Counter<long> KeysDeleted { get; }
        public Counter<long> BatchesProcessed { get; }
        public Counter<long> Retries { get; }
        public Counter<long> Failures { get; }
        public Histogram<double> BatchDurationMs { get; }
        public Histogram<long> BatchSize { get; }
        public UpDownCounter<long> InFlightBatches { get; }
        public Counter<long> QueuedRequests { get; }

        public RedisPrefixDeleteMetrics()
        {
            _meter = new Meter(MeterName, "1.0.0");

            KeysDeleted = _meter.CreateCounter<long>("redis_prefix_delete_keys_deleted");
            BatchesProcessed = _meter.CreateCounter<long>("redis_prefix_delete_batches_processed");
            Retries = _meter.CreateCounter<long>("redis_prefix_delete_retries");
            Failures = _meter.CreateCounter<long>("redis_prefix_delete_failures");
            BatchDurationMs = _meter.CreateHistogram<double>("redis_prefix_delete_batch_duration_ms");
            BatchSize = _meter.CreateHistogram<long>("redis_prefix_delete_batch_size");
            InFlightBatches = _meter.CreateUpDownCounter<long>("redis_prefix_delete_inflight_batches");
            QueuedRequests = _meter.CreateCounter<long>("redis_prefix_delete_queued_requests");
        }
    }
}
