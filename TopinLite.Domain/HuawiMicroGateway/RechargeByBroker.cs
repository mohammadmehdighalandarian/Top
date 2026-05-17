namespace TopinLite.Domain.HuawiMicroGateway
{
    public class RechargeByBrokerTcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
        public decimal Amount { get; set; }
        public string BrokerId { get; set; }
        public string RechargeChannelID { get; set; }
        public string TradeType { get; set; }
        public string BeId { get; set; }
    }
}