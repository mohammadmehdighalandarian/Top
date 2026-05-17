namespace TopinLite.Domain.TopinApi
{
    public class GetLoanEligibleOffersRequestModel
    {
        public string TelNum { get; set; }
        public string TelNumBuyer { get; set; }
        public string BizType { get; set; }
        public string ReqType { get; set; }
        public string Category { get; set; }
        public string Extra { get; set; }
        public string GateWay { get; set; }
        public string Reference { get; set; }
        public string UserId { get; set; }
    }

    public class GetLoanEligibleOffersResponseModel
    {
        public string ResponseDesc { get; set; }
        public string ResponseType { get; set; }
        public List<GetLoanEligibleOffersModel> Data { get; set; }
    }

    public class GetLoanEligibleOffersModel
    {
        public string OfferId { get; set; }
        public string OfferDesc { get; set; }
        public string OfferDescUssd { get; set; }
        public string OfferCost { get; set; }
        public string ConfirmText { get; set; }
        public string FreePackageNo { get; set; }
        public string Segment { get; set; }
    }
}