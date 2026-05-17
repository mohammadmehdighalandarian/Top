namespace TopinLite.Domain.TopinApi
{
    public class CheckBrokerValidityRequestModel
    {
        public string Amount { get; set; }
        public string AgentTelNum { get; set; }
        public string BrokerId { get; set; }
        public string UserId { get; set; }
        public string AgentId { get; set; }
        public string OrgId { get; set; }
        public string OrgName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}