namespace TopinLite.Domain.TopinApi
{
    public class BrokerSaleBankInquiryModel
    {
        public string BrokerUser { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string ProductType { get; set; }
        public object AdditionalData { get; set; }
    }
}