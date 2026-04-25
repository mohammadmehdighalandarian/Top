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
    }
}