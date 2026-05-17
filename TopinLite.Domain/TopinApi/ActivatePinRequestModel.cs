namespace TopinLite.Domain.TopinApi
{
    public class ActivatePinRequestModel
    {
        public decimal Telnum { get; set; }
        public string PinCode { get; set; }
        public decimal ChannelId { get; set; }
    }
}