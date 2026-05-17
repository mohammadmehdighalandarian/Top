using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.infra.PostgreSQL.Abstractions;

public interface ITopupSalesRepository
{
    Task InsertPackageSaleAsync(PackageSale item, CancellationToken cancellationToken = default);
    Task InsertPinlessChargeAsync(PinlessCharge item, CancellationToken cancellationToken = default);

    Task BulkInsertPackageSalesAsync(IReadOnlyCollection<PackageSale> items, CancellationToken cancellationToken = default);
    Task BulkInsertPinlessChargesAsync(IReadOnlyCollection<PinlessCharge> items, CancellationToken cancellationToken = default);
}
