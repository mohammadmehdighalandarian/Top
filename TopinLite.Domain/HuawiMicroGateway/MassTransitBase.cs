namespace TopinLite.Domain.HuawiMicroGateway
{
    public class MassTransitRequestBaseModel
    {
        public string ParametersStr { get; set; }
    }

    public interface IMassTransitRequestBaseModel
    {
        public string ParametersStr { get; set; }
    }

    public class MassTransitResponseBaseModel
    {
        public string MessageStr { get; set; }
        public string ResultCode { get; set; }
        public bool ExecStatus { get; set; }
    }
}