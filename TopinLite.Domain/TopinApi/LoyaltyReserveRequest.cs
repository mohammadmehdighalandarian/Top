namespace TopinLite.Domain.TopinApi;

public sealed class LoyaltyReserveRequest
{
    public string MobileNumber { get; set; }
    public string PackageNo { get; set; } 
    public string Gateway { get; set; }
    public decimal ProviderId { get; set; }

}

public sealed class LoyaltyResponse
{
    public decimal ResponseType { get; set; }
    public string ResponseDesc { get; set; }
}

