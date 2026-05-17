namespace TopinLite.Infra.InMemoryDb.Redis.Models
{
    public sealed class PrefixDeleteRequest
    {
        public required string Prefix { get; init; }
    }
}
