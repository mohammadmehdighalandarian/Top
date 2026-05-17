namespace TopinLite.Domain.Messaging
{
    public class QueryCustomerByTelRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
    }
    
    public class QueryCustomerByTelResponseModel : GeneralHuawiResponse
    {
    
    }
}