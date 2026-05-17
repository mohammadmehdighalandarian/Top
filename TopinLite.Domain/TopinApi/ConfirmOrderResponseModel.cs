namespace TopinLite.Domain.TopinApi
{
    public class ConfirmOrderResponseModel
    {
        public string AccountName { get; set; }
        public string Remain { get; set; }
        public int OfferCode { get; set; }
        public int LoyaltyYear { get; set; }
        public string TradeType { get; set; }
        public string RechargeSerialNo { get; set; }
        public string ResultRaw { get; set; }
    }
}