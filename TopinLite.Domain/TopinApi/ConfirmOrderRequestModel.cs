namespace TopinLite.Domain.TopinApi
{
    public class ConfirmOrderRequestModel
    {
        public decimal OrderId { get; set; }
        public decimal BankCode { get; set; }
        public string CardNo { get; set; }
        public decimal CardType { get; set; }
        public decimal RRN { get; set; }
    }
}