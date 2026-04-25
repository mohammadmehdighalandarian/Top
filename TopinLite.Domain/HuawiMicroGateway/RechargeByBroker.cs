namespace TopinLite.Domain.HuawiMicroGateway
{
    internal class RechargeByBroker
    {
    }

    public class RechargeByBrokerRequestModel : MassTransitRequestBaseModel
    {
    }

    public interface IRechargeByBrokerRequestModel : IMassTransitRequestBaseModel
    {
    }

    public class RechargeByBrokerResponseModel : MassTransitResponseBaseModel
    {
    }

    public class RechargeByBrokerTcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
        public long Amount { get; set; }
        public string BrokerId { get; set; }
        public string RechargeChannelID { get; set; }
        public string TradeType { get; set; }
        public string BeId { get; set; }
    }
}