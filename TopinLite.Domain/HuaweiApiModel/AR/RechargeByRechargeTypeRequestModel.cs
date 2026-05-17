namespace TopinLite.Domain.HuaweiApiModel.AR
{
    public class RechargeByRechargeTypeRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public long Amount { get; set; }
        public string MessageSeq { get; set; }
        public string RechargeType { get; set; }
    }
}