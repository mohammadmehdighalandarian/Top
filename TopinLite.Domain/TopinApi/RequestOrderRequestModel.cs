namespace TopinLite.Domain.TopinApi
{
    public class RequestOrderRequestModel : BrokerBaseModel
    {
        public decimal TelNum { get; set; }
        public decimal TelGift { get; set; }
        public string Amount { get; set; }
        public string ProductId { get; set; }
        public string payloadId { get; set; }

        // public string BrokerUser { get; set; }
        public string ChannelId { get; set; }

        public string Sms { get; set; }
        public string Voice { get; set; }
        public string Gprs { get; set; }

        public object AdditionalData { get; set; }
    }

    public class RequestOrderModel2 : BrokerBaseModel
    {
        public string TelNum { get; set; }
        public string TelGift { get; set; }
        public string Amount { get; set; }
        public string ProductId { get; set; }
        public string payloadId { get; set; }

        // public string BrokerUser { get; set; }
        public string ChannelId { get; set; }

        public string Sms { get; set; }
        public string Voice { get; set; }
        public string Gprs { get; set; }
    }

    // "TelNum":"9195579605",
    //"TelGift":"9195579605",
    //"Amount":"20000",
    //"ProductId":"1",
    //"payloadId":"1001",
    //"ChannelId":"11"
}