namespace TopinLite.infra.PostgreSQL.Models;

public sealed class PackageSale
{
    public decimal PkSeqPackageSales { get; set; }
    public decimal FkTelNum { get; set; }
    public decimal PackageAmount { get; set; }
    public decimal? ConfirmStatus { get; set; } = 0;
    public string? ConfirmDate { get; set; }
    public string? ConfirmTime { get; set; }
    public decimal? PayStatus { get; set; } = 0;
    public string? PayDate { get; set; }
    public string? PayTime { get; set; }
    public decimal? ResponseType { get; set; }
    public string? ResponseDesc { get; set; }
    public string? ProductCode { get; set; }
    public string? InsDate { get; set; }
    public string? InsTime { get; set; }
    public string? UpdDate { get; set; }
    public string? UpdTime { get; set; }
    public decimal? FkTelGift { get; set; }
    public decimal? FkPackageType { get; set; } = 0;
    public decimal? FkBank { get; set; }
    public decimal? ReserveStatus { get; set; }
    public string? Rrn { get; set; }
    public decimal IdCardType { get; set; }
    public string? IdCardNo { get; set; }
    public decimal? Retry { get; set; } = 0;
    public decimal? ChannelId { get; set; }
    public string? OrderId { get; set; }
    public decimal? FkBrokerId { get; set; }
    public DateTime InsTimestamp { get; set; } = DateTime.UtcNow;
    public DateTime? PayTimestamp { get; set; }
    public decimal? SapId { get; set; }
    public decimal? OfferCode { get; set; }
    public decimal? CrmMessageSequence { get; set; }
    public decimal? FkAccounts { get; set; }
    public decimal PkPackageSales { get; set; }
    public decimal? CampOrder { get; set; }
}
