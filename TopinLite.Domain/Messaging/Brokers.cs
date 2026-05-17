using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public class BrokersResponseModel : BrokersModel
    {
        public decimal SaleType { get; set; }
    }

    public class BrokersRequestModel
    {
        public decimal SapId { get; set; }
    }
}