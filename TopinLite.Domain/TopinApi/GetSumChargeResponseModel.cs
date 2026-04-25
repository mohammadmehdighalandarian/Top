namespace TopinLite.Domain.TopinApi
{
    public class GetSumChargeResponseModel
    {
        public string Status { get; set; }
        public string Success { get; set; }
        public string SuccessTotalAmount { get; set; }
        public string Fail { get; set; }
        public string FailTotalAmount { get; set; }
    }
}