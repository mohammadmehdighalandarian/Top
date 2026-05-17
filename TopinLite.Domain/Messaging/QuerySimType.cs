namespace TopinLite.Domain.Messaging
{
    public class QuerySimTypeRequestModel
    {
        public string SubscriberNo { get; set; }
        public string Mss { get; set; }
    }
    
    public class QuerySimTypeResponseModel : GeneralHuawiResponse
    {
    
    }
}