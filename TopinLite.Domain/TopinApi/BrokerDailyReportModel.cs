namespace TopinLite.Domain.TopinApi
{
    public class BrokerDailyReportModel
    {
        public List<decimal> BrokerId { get; set; }
        public string ProductType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}