namespace TopinLite.Domain.LogModels
{
    public class UserActivityCallLogModel
    {
        public string TraceIdentifier { get; set; } // httprequest TraceIdentifier
        public string Path { get; set; } // endpoint
        public string Verb { get; set; } // http method like post , put , delete , get
        public int Direction { get; set; } // 1 request , 2 response
        public int StatusCode { get; set; }
        public string Body { get; set; }
        public object Headers { get; set; }
        public string HeadersString { get; set; }
        public long EventDate { get; set; }
        public string ConnectionInfo { get; set; }
        public string ConsumerKey { get; set; }
        public string Origin { get; set; }
    }
}