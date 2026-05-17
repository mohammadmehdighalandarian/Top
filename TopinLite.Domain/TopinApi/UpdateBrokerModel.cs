namespace TopinLite.Domain.TopinApi
{
    public class UpdateBrokerModel
    {
        public decimal? P_CUSTOMER_ID { get; set; }
        public decimal? P_VENDOR_ID { get; set; }
        public string P_BROKER_DESC { get; set; }
        public decimal P_BROKER_TYPE { get; set; }
        public string P_USER { get; set; }
        public string P_AADITIONAL_DATA { get; set; }
    }
}