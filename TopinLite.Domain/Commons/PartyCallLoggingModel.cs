namespace TopinLite.Domain.Commons
{
    public class PartyCallLoggingModel
    {
        public string From { get; set; }
        public string TraceId { get; set; }
        public string RequestString { get; set; }
        public string ResponseString { get; set; }
        public string RequestURL { get; set; }
        public long ElapsedMS { get; set; }
        public int ResponseCode { get; set; }
    }
}