namespace TopinLite.Domain.Messaging
{
    public class QuerySubscriberRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
    }

    public class QuerySubscriberResponseModel : GeneralHuawiResponse
    {

    }
}
