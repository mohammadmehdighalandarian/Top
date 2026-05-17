namespace TopinLite.Biz.PackageSaleHandler.PackageOrder;

public sealed class PackageOrderContext
{
    public decimal ProviderId { get; set; }
    public decimal TelNum { get; set; }
    public decimal TelGift { get; set; }
    public decimal Amount { get; set; }
    public int OfferId { get; set; }
    public decimal OfferCode { get; set; }
    public decimal BrokerId { get; set; }
    public int ChannelId { get; set; }
    public int SapId { get; set; }
    public decimal AccountId { get; set; }
    public decimal CampOrder { get; set; }
    public decimal ReserveStatus { get; set; }
    public decimal Category { get; set; }
    public int? SaleType { get; set; }
    public decimal Data { get; set; }
    public decimal Voice { get; set; }
    public decimal Sms { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
