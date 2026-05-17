namespace TopinLite.Domain.TopinDatabaseModels
{
    public class BrokersModel
    {
        public string StrPass { get; set; }
        public decimal SapId { get; set; }
        public string FkBroker { get; set; }
        public decimal BrokerId { get; set; }
        public decimal BrokerType { get; set; }
        public decimal Status { get; set; }
        public decimal SaleAccess { get; set; }


        public int? SaleType { get; set; }
        public decimal? Credit { get; set; }
        public decimal? DailyLimitation { get; set; }
        public decimal? MonthlyLimitation { get; set; }
        public decimal? DefaultAccountId { get; set; }
        public string? DefaultAccountName { get; set; }
        public string? BrokerDesc { get; set; }
    }
}