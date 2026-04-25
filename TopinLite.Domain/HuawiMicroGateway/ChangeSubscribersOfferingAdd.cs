namespace TopinLite.Domain.HuawiMicroGateway
{
    internal class ChangeSubscribersOfferingAdd
    {
    }

    //public class ChangeSubscribersOfferingAddRequestModel : MassTransitRequestBaseModel
    //{
    //}

    //public interface IChangeSubscribersOfferingAddRequestModel : IMassTransitRequestBaseModel
    //{
    //}

    //public class ChangeSubscribersOfferingAddResponseModel : MassTransitResponseBaseModel
    //{
    //}

    public class ChangeSubscribersOfferingAddTcpRequest
    {

        public string SponserMsisdn { get; set; }
        public string PrimaryIdentity { get; set; }
        public string OfferingId { get; set; }
        public string Mss { get; set; }
        public string BrokerId { get; set; }
        public string CycleDiscount { get; set; }
        public string Data { get; set; }
        public string Voice { get; set; }
        public string SMS { get; set; }
    }
}