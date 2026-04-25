using System;
using System.Collections.Generic;
using System.Text;

namespace TopinLite.Domain.Messaging
{
    public class ChangeSubscribersOfferingAddRequestModel
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



    public class ChangeSubscribersOfferingAddResponseModel : GeneralHuawiResponse
    {
    
    }

}
