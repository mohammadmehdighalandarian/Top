using TopinLite.Domain.Messaging;

namespace TopinLite.Domain.Commons;

public sealed class PackageRequestModel
{
    public PackageRequestModel(ParsedRequest request, OfferResponseModel offer, BrokersResponseModel broker)
    {
        Request = request;
        Offer = offer;
        Broker = broker;
    }

    public ParsedRequest Request { get; }
    public OfferResponseModel Offer { get; }
    public BrokersResponseModel Broker { get; }
    public decimal AccountId { get; set; }
    public string AccountName { get; set; }
    public string CampOrder { get; set; } = "0";
    public string LastIntegrationResponse { get; set; } = string.Empty;

}

public sealed class ParsedRequest
{
    public decimal TelNum { get; set; }
    public decimal TelGift { get; set; }
    public decimal Amount { get; set; }
    public int OfferId { get; set; }
    public int SapId { get; set; }
    public int ChannelId { get; set; }
    public int? SaleType { get; set; }
    public decimal Data { get; set; }
    public decimal Voice { get; set; }
    public decimal Sms { get; set; }
}


