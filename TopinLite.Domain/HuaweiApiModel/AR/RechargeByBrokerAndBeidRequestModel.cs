namespace TopinLite.Domain.HuaweiApiModel.AR
{
    public class RechargeByBrokerAndBeidRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public long Amount { get; set; }
        public string MessageSeq { get; set; }
        public string RechargeType { get; set; }
        public string BrokerId { get; set; }
        public string Beid { get; set; }
        public string RechargeChannelId { get; set; }
    }
}