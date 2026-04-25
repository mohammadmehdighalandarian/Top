namespace TopinLite.Domain.TopinApi
{
    public class SubscriberTransactionResModel
    {
        public string SapId { get; set; }
        public string BrokerId { get; set; }
        public string BrokerDesc { get; set; }
        public string TelGift { get; set; }
        public string TelNum { get; set; }
        public string Amount { get; set; }
        public string ChargePackageDesc { get; set; }
        public string PayDate { get; set; }
        public string PayTime { get; set; }
        public string InsDate { get; set; }
        public string InsTime { get; set; }
        public string RRN { get; set; }
        public string ResponseDesc { get; set; }
        public string ResponseType { get; set; }
        public string TrackCode { get; set; }
        public string BankName { get; set; }
    }
}