namespace TopinLite.Domain.TopinApi
{
    public class GetChargeListRequestModel
    {
        public string OrderId { get; set; }
        public string BrokerId { get; set; }
        public string PayDate { get; set; }
        public List<TelList> telList { get; set; }

        public string RRN { get; set; }
        public string BankCode { get; set; }
        public string PayId { get; set; }
        public string ChannelID { get; set; }
    }

    public class TelList
    {
        public string tel { get; set; }
        public string amount { get; set; }
    }
}