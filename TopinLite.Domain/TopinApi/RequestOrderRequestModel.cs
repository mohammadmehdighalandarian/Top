namespace TopinLite.Domain.TopinApi
{
    public class RequestOrderRequestModel : BrokerBaseModel
    {
        public decimal TelNum { get; set; }
        public decimal TelGift { get; set; }
        public string Amount { get; set; }
        public string ProductId { get; set; }
        public string PayloadId { get; set; }
        public string ChannelId { get; set; }
        public string Sms { get; set; }
        public string Voice { get; set; }
        public string Gprs { get; set; }
        public object AdditionalData { get; set; }
    }
}