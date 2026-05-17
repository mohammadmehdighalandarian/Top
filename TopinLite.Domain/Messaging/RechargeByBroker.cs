namespace TopinLite.Domain.Messaging
{
    public class RechargeByBrokerRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
        public decimal Amount { get; set; }
        public string BrokerId { get; set; }
        public string RechargeChannelID { get; set; }
        public string TradeType { get; set; }
        public string BeId { get; set; }
    }
    
    public class RechargeByBrokerResponseModel : GeneralHuawiResponse
    {
    
    }
}