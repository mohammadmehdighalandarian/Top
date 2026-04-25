using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public class BrokersResponseModel : BrokersModel
    {
    }

    public class BrokersRequestModel
    {
        public int SapId { get; set; }
    }
}