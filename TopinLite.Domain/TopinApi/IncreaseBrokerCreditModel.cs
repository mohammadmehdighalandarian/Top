namespace TopinLite.Domain.TopinApi
{
    public class IncreaseBrokerCreditModel
    {
        public decimal BrokerUser { get; set; }
        public decimal Credit { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceTime { get; set; }
        public object AdditionalData { get; set; }
    }
}