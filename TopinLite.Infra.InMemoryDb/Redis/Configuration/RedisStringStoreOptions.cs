namespace TopinLite.Infra.InMemoryDb.Redis.Configuration
{
    public sealed class RedisStringStoreOptions
    {
        public const string SectionName = "RedisStringStore";

        public string KeyPrefix { get; set; } = string.Empty;
        public int DefaultDatabase { get; set; } = 0;
    }
}
