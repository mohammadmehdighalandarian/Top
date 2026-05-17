using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using TopinLite.Biz.PackageSaleHandler.PackageOrder;

namespace TopinLite.Biz.PackageSaleHandler.ServiceProviders;

public sealed class PackageOrderStoreOptions
{
    public const string SectionName = "PackageOrderStore";

    public string PackageSaleKeyFormat { get; set; } = "topin:package:sale:{0}";
    public int TtlSeconds { get; set; } = 172800;
}

public interface IPackageOrderStore
{
    Task SavePackageSaleAsync(PackageOrderContext record, CancellationToken cancellationToken);
    Task<PackageOrderContext?> GetPackageSaleAsync(decimal providerId, CancellationToken cancellationToken);

}
public sealed class RedisPackageOrderStore : IPackageOrderStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = null
    };

    private readonly IConnectionMultiplexer _redis;
    private readonly PackageOrderStoreOptions _options;

    public RedisPackageOrderStore(IConnectionMultiplexer redis, IOptions<PackageOrderStoreOptions> options)
    {
        _redis = redis;
        _options = options.Value;
    }

    public async Task SavePackageSaleAsync(PackageOrderContext record, CancellationToken cancellationToken)
    {
        IDatabase db = _redis.GetDatabase();
        string key = string.Format(_options.PackageSaleKeyFormat, record.ProviderId);
        string json = JsonSerializer.Serialize(record, JsonOptions);

        await db.StringSetAsync(key, json, TimeSpan.FromSeconds(_options.TtlSeconds)).ConfigureAwait(false);
    }

    public async Task<PackageOrderContext?> GetPackageSaleAsync(decimal providerId, CancellationToken cancellationToken)
    {
        IDatabase db = _redis.GetDatabase();
        string key = string.Format(_options.PackageSaleKeyFormat, providerId);
        RedisValue raw = await db.StringGetAsync(key).ConfigureAwait(false);

        if (!raw.HasValue)
            return null;

        try
        {
            return JsonSerializer.Deserialize<PackageOrderContext>(raw.ToString(), JsonOptions);
        }
        catch
        {
            return null;
        }
    }



}
