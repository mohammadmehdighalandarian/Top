namespace TopinLite.Domain.TopinApi
{
    public class ActivePackageOrderRequestModel
    {
        public decimal? OfferId { get; set; }
        public decimal? Amount { get; set; }
        public string UserIns { get; set; }
        public List<TelNumList> TelList { get; set; }
        public string OrgId { get; set; }
        public string OrgName { get; set; }
        public decimal? Sms { get; set; }
        public decimal? SmsAmount { get; set; }
        public decimal? Voice { get; set; }
        public decimal? VoiceAmount { get; set; }
        public string ChannelID { get; set; }
    }

    public class TelNumList
    {
        public string TelNum { get; set; }
    }
}