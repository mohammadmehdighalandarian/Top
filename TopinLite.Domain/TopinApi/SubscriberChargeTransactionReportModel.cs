namespace TopinLite.Domain.TopinApi
{
    public class SubscriberChargeTransactionReportModel
    {
        public string ProductType { get; set; }
        public decimal? TelNum { get; set; }
        public decimal? TelGift { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<decimal> BrokerId { get; set; }
    }
}