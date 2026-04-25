namespace TopinLite.Domain.TopinApi
{
    public class AccountDetailInquiryModel
    {
        public string UserId { get; set; }
        public string AccountName { get; set; }
    }

    public class AccountDetailInquiryResModel
    {
        public string Credit { get; set; }
        public string UsedAmount { get; set; }
        public string DailyLimitation { get; set; }
        public string RemaindCredit { get; set; }
    }
}