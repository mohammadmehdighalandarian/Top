namespace TopinLite.Domain.HuawiMicroGateway
{
    internal class QueryBalance
    {
    }

    public class QueryBalanceRequestModel : MassTransitRequestBaseModel
    {
    }

    public interface IQueryBalanceRequestModel : IMassTransitRequestBaseModel
    {
    }

    public class QueryBalanceResponseModel : MassTransitResponseBaseModel
    {
    }

    public class QueryBalanceTcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
    }
}