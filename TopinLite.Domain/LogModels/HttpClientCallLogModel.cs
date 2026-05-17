namespace TopinLite.Domain.LogModels
{
    public class HttpClientCallLogModel
    {
        public string Url { get; set; }
        public string RequestBody { get; set; }
        public string RequestHeader { get; set; }
        public string ResponseBody { get; set; }
        public string ResponseHeader { get; set; }
        public long ElapsedMilliseconds { get; set; }
        public int HttpStatusCode { get; set; }
        public string From { get; set; }
    }
}