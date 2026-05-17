using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public class SelectedBrokerResponseModel: SelectedBrokerModel
    {
    }

    public class SelectedBrokerRequestModel
    {
        public decimal BrokerId { get; set; }
        public decimal OfferId { get; set; }
    }
}
