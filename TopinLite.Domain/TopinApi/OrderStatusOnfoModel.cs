namespace TopinLite.Domain.TopinApi
{
    public class OrderStatusOnfoModel
    {
        public int SapId { get; set; }
        public int OrderType { get; set; }
        public int ProviderId { get; set; }
        public long fromTimeStamp { get; set; }
        public long toTimeStamp { get; set; }
    }
}