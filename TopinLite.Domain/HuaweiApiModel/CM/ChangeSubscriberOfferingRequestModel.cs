namespace TopinLite.Domain.HuaweiApiModel.CM
{
    public class ChangeSubscriberOfferingRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public string MessageSeq { get; set; }

        public string OfferId { get; set; }
        public string BrokerId { get; set; }
    }
}