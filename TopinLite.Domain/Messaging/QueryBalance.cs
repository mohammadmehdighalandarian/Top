namespace TopinLite.Domain.Messaging
{
    public class QueryBalanceRequestModel
    {
        public string PrimaryIdentity { get; set; }
        public string Mss { get; set; }
    }

    public class QueryBalanceResponseModel : GeneralHuawiResponse
    {
        
    }
}