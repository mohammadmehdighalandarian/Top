namespace TopinLite.Domain.HuawiMicroGateway
{

    public class IntegrationEnquiryRequestModel : MassTransitRequestBaseModel
    {

    }

    public interface IIntegrationEnquiryRequestModel : IMassTransitRequestBaseModel
    {

    }

    public class IntegrationEnquiryResponseModel : MassTransitResponseBaseModel
    {

    }

    public class IntegrationEnquiryTcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }

        public List<IntegrationEnquiryTcpRequestPrimaryOffers> PrimaryOffers { get; set; }

        public List<Domain.HuaweiApiModel.CRMResponses.PrimaryOfferWhiteList> PrimaryOfferWhiteLists { get; set; }
    }


    public class IntegrationEnquiryTcpRequestPrimaryOffers
    {
        public long Offer { get; set; }
        public long Type { get; set; }
    }
}
