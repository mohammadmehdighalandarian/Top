using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging;

public class BrokerSaleLimitResponseModel : BrokerSaleLimitModel
{
    
}

public class BrokerSaleLimitRequestModel
{
    public decimal BrokerId { get; set; }
}