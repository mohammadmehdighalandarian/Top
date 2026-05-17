using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging;

public class BrokerOfferAccessResponseModel: BrokerOfferAccessModel
{
}
public class BrokerOfferAccessRequestModel
{
    public decimal BrokerId { get; set; }
    public decimal OfferId { get; set; }
}
