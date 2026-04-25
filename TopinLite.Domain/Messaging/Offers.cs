using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public sealed class OfferResponseModel : OffersModel
    {
    }

    public sealed class OfferRequestModel
    {
        public int OfferId { get; set; }
    }
}