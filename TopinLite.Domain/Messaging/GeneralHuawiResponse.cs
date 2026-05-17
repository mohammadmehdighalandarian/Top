namespace TopinLite.Domain.Messaging
{
    public class GeneralHuawiResponse
    {
        public string ResponseType { get; set; }
        public string ResponseDesc { get; set; }
    }

    public class GeneralHuawiResponse<T> : GeneralHuawiResponse
    {
        public T Data { get; set; }
    }
}