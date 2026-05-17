namespace TopinLite.Domain.TopinApi
{
    public class GetHamrahiPackagesInfoModel
    {
        public string MobileNumber { get; set; }
        public string BizType { get; set; }
        public string IsFree { get; set; }
        public string Channel { get; set; }
        public string category { get; set; }
    }

    public class GetHamrahiPackagesInfoV1
    {
        public string TelNum { get; set; }
        public string BizType { get; set; }
        public string IsFree { get; set; }
        public string ChannelId { get; set; }
        public string Gateway { get; set; }
    }
}