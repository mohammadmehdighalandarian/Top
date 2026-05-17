namespace TopinLite.Domain.HuawiMicroGateway
{

    public class IntegrationEnquiryTcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }

        public List<IntegrationEnquiryTcpRequestPrimaryOffers> PrimaryOffers { get; set; }

        public List<Domain.HuaweiApiModel.CRMResponses.PrimaryOfferWhiteList> PrimaryOfferWhiteLists { get; set; }
    }


    public class IntegrationEnquiryTcpRequestPrimaryOffers
    {
        public decimal Offer { get; set; }
        public decimal Type { get; set; }
    }
}
