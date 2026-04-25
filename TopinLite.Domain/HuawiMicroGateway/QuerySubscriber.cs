namespace TopinLite.Domain.HuawiMicroGateway
{
    public class QuerySubscriberRequestModel : MassTransitRequestBaseModel
    {
    }

    public interface IQuerySubscriberRequestModel : IMassTransitRequestBaseModel
    {
    }

    public class QuerySubscriberResponseModel : MassTransitResponseBaseModel
    {
    }

    public class QuerySubscriberTcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
    }
}