using System;
using System.Collections.Generic;
using System.Text;
using TopinLite.Domain.HuawiMicroGateway;

namespace TopinLite.Domain.Messaging
{
    public class IntegrationEnqueryRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }

        public List<IntegrationEnquiryRequestPrimaryOffers> PrimaryOffers { get; set; }

        public List<IntegrationEnquiryPrimaryOfferWhiteLists> PrimaryOfferWhiteLists { get; set; }
    }

    public class IntegrationEnquiryRequestPrimaryOffers
    {
        public decimal Offer { get; set; }
        public decimal Type { get; set; }
    }

    public class IntegrationEnquiryPrimaryOfferWhiteLists
    {
        public decimal offerId { get; set; }
        public int type { get; set; }
    }

    public class IntegrationEnqueryResponseModel : GeneralHuawiResponse
    {

    }
}
