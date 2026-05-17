using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public class MessagesResponseModel : MessagesModel
    {
    }

    public class MessagesRequestModel
    {
        public decimal MessageId { get; set; }
        public string MethodName { get; set; }
    }
}
