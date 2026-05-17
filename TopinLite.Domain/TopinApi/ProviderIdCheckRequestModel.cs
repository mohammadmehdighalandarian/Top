namespace TopinLite.Domain.TopinApi;

public sealed class ProviderIdCheckRequestModel
{
    public decimal ProviderId { get; set; }
    public int Type { get; set; }
}

public sealed class ProviderIdCheckResponseModel
{
    public decimal OfferId { get; set; }
    public decimal BrokerId { get; set; }
    public decimal Amount { get; set; }
    public decimal ResponseType { get; set; }
}
