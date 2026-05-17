namespace TopinLite.Domain.TopinApi
{
    public class BrokerSaleInquiryModel
    {
        public decimal BrokerUser { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string ProductId { get; set; }
        public object AdditionalData { get; set; }
    }
}