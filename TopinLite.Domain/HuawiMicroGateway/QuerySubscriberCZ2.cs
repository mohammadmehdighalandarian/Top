namespace TopinLite.Domain.HuawiMicroGateway
{
    public class QuerySubscriberCZ2RequestModel : MassTransitRequestBaseModel
    {
    }

    public interface IQuerySubscriberCZ2RequestModel : IMassTransitRequestBaseModel
    {
    }

    public class QuerySubscriberCZ2ResponseModel : MassTransitResponseBaseModel
    {
    }

    public class QuerySubscriberCZ2TcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }

        public string ProviderId { get; set; }
    }
}