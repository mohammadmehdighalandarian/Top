namespace TopinLite.Domain.TopinApi
{
    public class HamrahBonCheckBrokerValidityRequestModel
    {
        public decimal? P_AMOUNT { get; set; }
        public decimal? P_ORG_ID { get; set; }
        public string P_ORG_NAME { get; set; }
        public decimal? P_AGENT_ID { get; set; }
        public string P_AGENT_NAME { get; set; }
        public string P_USER_INS { get; set; }
        public string P_PAYABLE_AMOUNT { get; set; }
    }
}