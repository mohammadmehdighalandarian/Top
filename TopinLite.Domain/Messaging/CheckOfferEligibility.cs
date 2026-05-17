namespace TopinLite.Domain.Messaging
{
    public class CheckOfferEligibilityRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
        public string OfferId { get; set; }
    }

    public class CheckOfferEligibilityResponseModel : GeneralHuawiResponse
    {
    
    }
}