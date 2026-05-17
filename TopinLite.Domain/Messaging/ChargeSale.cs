using TopinLite.Domain.TopinApi;

namespace TopinLite.Domain.Messaging
{
    public sealed class ChargeRequestOrderRequest : RequestOrderRequestModel
    {
    }
    
    public sealed class ChargeRequestOrderResponse
    {
        public decimal OrderId { get; set; }
    }
    
    public sealed class ChargeConfirmOrderRequest
    {
        public decimal OrderId { get; set; }
        public decimal BankCode { get; set; }
        public string CardNo { get; set; } = string.Empty;
        public decimal CardType { get; set; }
        public decimal RRN { get; set; }
    }
 
    public sealed class ChargeConfirmOrderResponse : ConfirmOrderResponseModel
    {
    }

    public sealed class ChargeTypeValidateRequest
    {
        public decimal TelNum { get; set; }
        public decimal TelGift { get; set; }
        public string Amount { get; set; } = string.Empty;
        public string PayloadId { get; set; } = string.Empty;
        public string ChannelId { get; set; } = string.Empty;
        public string BrokerId { get; set; } = string.Empty;
        public decimal CustomerId { get; set; }
        public decimal VendorId { get; set; }
        public object? AdditionalData { get; set; }
    }
 
    public sealed class ChargeTypeValidateResponse
    {
        public bool ExecStatus { get; set; }
        public decimal ResultCode { get; set; }
        public string ResultMessage { get; set; } = string.Empty;
    }
}