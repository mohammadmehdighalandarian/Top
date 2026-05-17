namespace TopinLite.Domain.Commons;

// Changed underlying type from bool to int to fix CS1008
public enum ExecStatus : int
{
    Success = 1,
    Failed = 0
}


public static class MemoryDataKeys
{
    public const string Offers = "Offers:";
    public const string Brokers = "Brokers:";
    public const string BrokersAccess = "BrokersAccess:";
    public const string BrokerOfferAccess = "BrokerOfferAccess:";
    public const string DynamicCondition = "DynamicCondition:";
    public const string DiyPrice = "DiyPrice:";
    public const string DiyDataAccess = "DiyDataAccess:";
    public const string PrimaryOffers = "PrimaryOffers:";
    public const string BrokerSaleLimits = "BrokerSaleLimits:";
    public const string SelectedBrokers = "SelectedBrokers:";
    public const string TradeTypes = "TradeTypes:";
    public const string BrokerSmsTexts = "BrokerSmsTexts:";
    public const string Messages = "Messages:";
}

public enum Products
{
    Charge = 1,
    Package = 2,
    Freeze = 3,
    Anarestan = 4
}
