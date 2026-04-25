namespace TopinLite.Domain.TopinApi
{
    public class BrokerDailyChargeReportResponseModel
    {
        public string SaleDate { get; set; }
        public string BrokerId { get; set; }
        public string BrokerDesc { get; set; }
        public string TotalSale { get; set; }
        public string DirectCharge { get; set; }
        public string DesiredCharge { get; set; }
        public string AmazingCharge { get; set; }
        public string YouthCharge { get; set; }
        public string WomenCharge { get; set; }
        public string LoyaltyCharge { get; set; }
    }
}