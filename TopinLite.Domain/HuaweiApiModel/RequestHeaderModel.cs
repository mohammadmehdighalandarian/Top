namespace TopinLite.Domain.HuaweiApiModel
{
    public class RequestHeaderModel
    {
        public string Version { get; set; }
        public string BusinessCode { get; set; }
        public string MessageSeq { get; set; }
        public string ChannelType { get; set; }
        public OwnershipInfoModel OwnershipInfo { get; set; }
        public OperatorInfoModel OperatorInfo { get; set; }
        public AccessSecurityModel AccessSecurity { get; set; }
    }

    public class OwnershipInfoModel
    {
        public string BeId { get; set; }
        public string BRID { get; set; }
        public string RegionId { get; set; }
    }

    public class OperatorInfoModel
    {
        public string OperatorId { get; set; }
        public string ChannelId { get; set; }
        public string OrgId { get; set; }
    }

    public class AccessSecurityModel
    {
        public string LoginSystemCode { get; set; }
        public string Password { get; set; }
    }
}