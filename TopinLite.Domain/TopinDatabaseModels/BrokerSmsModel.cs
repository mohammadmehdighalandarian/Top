namespace TopinLite.Domain.TopinDatabaseModels
{
    public class BrokerSmsModel
    {
        public decimal SmsId { get; set; }
        public decimal BrokerId { get; set; }
        public decimal RequestType { get; set; }
        public string SmsText { get; set; }
    }
}
