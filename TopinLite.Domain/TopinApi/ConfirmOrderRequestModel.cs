namespace TopinLite.Domain.TopinApi
{
    public class ConfirmOrderRequestModel
    {
        public decimal OrderId { get; set; }
        public string BankCode { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public decimal RRN { get; set; }
    }
}