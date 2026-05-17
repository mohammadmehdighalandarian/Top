namespace TopinLite.Domain.TopinApi;

public sealed class YouthConfirmOrderResponseModel : ConfirmOrderResponseModel
{
    public decimal TradeType { get; set; }
    public string RechargeSerialNo { get; set; }
    public decimal LoyaltyYear { get; set; }

}
