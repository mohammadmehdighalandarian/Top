namespace TopinLite.Domain.TopinApi
{
    public class InformRechargeModel
    {
        public decimal SapId { get; set; }
        public string TelNum { get; set; }
        public int IsSuccess { get; set; }
        public decimal EventUniqId { get; set; }
        public string Description { get; set; }
    }

    public class InformRechargeResModel
    {
        public string TrackCode { get; set; }
    }
}