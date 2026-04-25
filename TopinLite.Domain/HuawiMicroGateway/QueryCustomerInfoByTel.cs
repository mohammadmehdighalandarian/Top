namespace TopinLite.Domain.HuawiMicroGateway
{
    public class QueryCustomerInfoByTelRequestModel : MassTransitRequestBaseModel
    {
    }

    public interface IQueryCustomerInfoByTelRequestModel : IMassTransitRequestBaseModel
    {
    }

    public class QueryCustomerInfoByTelResponseModel : MassTransitResponseBaseModel
    {
    }

    public class QueryCustomerInfoByTelTcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
    }
}