namespace TopinLite.Domain.TopinDatabaseModels;

public class BrokerSaleLimitModel
{
    public decimal RetryLimit { get; set; }
    public decimal TimeLimit { get; set; }
    public decimal BrokerId { get; set; }
}