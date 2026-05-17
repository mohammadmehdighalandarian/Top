namespace TopinLite.infra.PostgreSQL.Models;

public sealed class PinlessCharge
{
    public decimal PkSeqPinlessCharge { get; set; }
    public decimal FkTelNum { get; set; }
    public decimal ChargeAmount { get; set; }
    public decimal? ConfirmStatus { get; set; } = 0;
    public string? ConfirmDate { get; set; }
    public string? ConfirmTime { get; set; }
    public decimal? ChargeStatus { get; set; } = 0;
    public string? ChargeDate { get; set; }
    public string? ChargeTime { get; set; }
    public decimal? ResponseType { get; set; }
    public string? ResponseDesc { get; set; }
    public string? InsDate { get; set; }
    public string? InsTime { get; set; }
    public string? UpdDate { get; set; }
    public string? UpdTime { get; set; }
    public decimal? FkTelCharger { get; set; }
    public decimal? FkChargeType { get; set; } = 0;
    public decimal? FkBank { get; set; }
    public decimal? FkCrmSeq { get; set; }
    public decimal? Retry { get; set; }
    public string? Rrn { get; set; }
    public decimal? IdCardType { get; set; }
    public string? IdCardNo { get; set; }
    public string? RechargeSerialNo { get; set; }
    public decimal? ChannelId { get; set; }
    public decimal? FkBrokerId { get; set; }
    public DateTime InsTimestamp { get; set; } = DateTime.UtcNow;
    public DateTime? ChargeTimestamp { get; set; }
    public decimal? SapId { get; set; }
    public decimal? FkAccounts { get; set; }
    public decimal? OfferCode { get; set; }
    public decimal PkPinlessCharge { get; set; }
    public string? BankUniqId { get; set; }
}
