namespace TopinLite.Infra.InMemoryDb.Redis.Configuration
{
    public sealed class RedisPrefixDeleteOptions
    {
        public const string SectionName = "RedisPrefixDelete";

        public int Database { get; set; } = 0;
        public int ScanPageSize { get; set; } = 1000;
        public int DeleteBatchSize { get; set; } = 500;
        public int MaxParallelBatchesPerServer { get; set; } = 4;
        public bool PreferUnlink { get; set; } = true;

        // Retry
        public int MaxRetries { get; set; } = 3;
        public int RetryDelayMs1 { get; set; } = 100;
        public int RetryDelayMs2 { get; set; } = 300;
        public int RetryDelayMs3 { get; set; } = 750;

        // Distributed lock
        public string LockKeyPrefix { get; set; } = "lock:redis-prefix-delete:";
        public TimeSpan LockExpiry { get; set; } = TimeSpan.FromMinutes(10);

        // Queue
        public int QueueCapacity { get; set; } = 500;

        // Prefix safety
        public int MinimumPrefixLength { get; set; } = 3;
    }
}
