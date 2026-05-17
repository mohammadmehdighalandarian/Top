using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public class BrokerSmsResponseModel : BrokerSmsModel
    {
    }

    public class BrokerSmsRequestModel
    {
        public decimal BrokerId { get; set; }
        public decimal RequestType { get; set; }
        public decimal SmsId { get; set; }
    }
}
