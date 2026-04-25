namespace TopinLite.Domain.HuaweiApiModel.AR
{
    public class RechargeRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public long Amount { get; set; }
        public string MessageSeq { get; set; }
    }
}