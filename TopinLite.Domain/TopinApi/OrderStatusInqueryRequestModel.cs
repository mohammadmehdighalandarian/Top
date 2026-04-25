namespace TopinLite.Domain.TopinApi
{
    public class OrderStatusInqueryRequestModel
    {
        public decimal BrokerUser { get; set; }
        public string TelNum { get; set; }
        public string OrderId { get; set; }
        public string BankCode { get; set; }
        public string ProductId { get; set; }
    }
}