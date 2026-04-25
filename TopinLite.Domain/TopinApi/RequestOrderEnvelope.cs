namespace TopinLite.Domain.TopinApi
{
    public class RequestOrderEnvelope
    {
        public RequestOrderRequestModel Request { get; set; }
        public string ClientIp { get; set; }
        public string TraceId { get; set; }
        public DateTime RequestedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
