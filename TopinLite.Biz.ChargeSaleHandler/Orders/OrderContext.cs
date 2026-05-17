namespace TopinLite.Biz.ChargeSaleHandler.Orders;

public sealed class OrderContext
{
    public decimal PkSeqPinlessCharge { get; set; }
    public decimal PkPinlessCharge { get; set; }
    public decimal TelNum { get; set; }
    public decimal ChargeAmount { get; set; }
    public decimal FkChargeType { get; set; }
    public decimal FkBrokerId { get; set; }
    public decimal FkAccounts { get; set; }
    public decimal FkCrmSeq { get; set; }
    public decimal FkBank { get; set; }
    public decimal FkTelCharger { get; set; }
    public decimal SapId { get; set; }
    public decimal ChannelId { get; set; }
    public decimal OfferCode { get; set; }
    public short ConfirmStatus { get; set; }
    public string? ConfirmDate { get; set; }
    public string? ConfirmTime { get; set; }
    public short ChargeStatus { get; set; }
    public string? ChargeDate { get; set; }
    public string? ChargeTime { get; set; }
    public decimal ResponseType { get; set; }
    public string? ResponseDesc { get; set; }
    public string? Rrn { get; set; }
    public string? BankUniqId { get; set; }
    public string? RechargeSerialNo { get; set; }
    public short IdCardType { get; set; }
    public string? IdCardNo { get; set; }
    public string? InsDate { get; set; }
    public string? InsTime { get; set; }
    public DateTimeOffset InsTimestamp { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ChargeTimestamp { get; set; }
    public string? UpdDate { get; set; }
    public string? UpdTime { get; set; }
    public decimal Retry { get; set; }
    public string ChargeType { get; set; } = string.Empty;
    public string PayloadId { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}