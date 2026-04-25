namespace TopinLite.Domain.TopinApi
{
    public class ReserveBrokerCreditRequestModel
    {
        public decimal? OrderId { get; set; }
        public string RRN { get; set; }
        public string CardType { get; set; }
        public string CardNo { get; set; }
        public string UserIns { get; set; }
        public decimal? BankId { get; set; }
        public decimal? ResultStatus { get; set; }
        public string ResultDesc { get; set; }
        public string TrackingNo { get; set; }
    }
}