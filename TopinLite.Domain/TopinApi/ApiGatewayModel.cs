using Newtonsoft.Json;

namespace TopinLite.Domain.TopinApi
{
    public class ApiGatewayModel<T>
    {
        public string AppletMap { get; set; }
        public T Payload { get; set; }
        public string TrackCode { get; set; }
        public decimal Flag { get; set; }
        public decimal AppletId { get; set; }
        public string CardCode { get; set; }
        public object Authorization { get; set; }
    }

    public class ChargePayload
    {
        public decimal payloadId { get; set; }
        public decimal productId { get; set; }
        public decimal payloadAmount { get; set; }
        public decimal sourceMSISDN { get; set; }
        public decimal destinationMSISDN { get; set; }
        public decimal payloadChargeType { get; set; }
        public string apiAuthUsername { get; set; }
        public string apiAuthPassword { get; set; }
        public decimal payloadPackageType { get; set; }
    }

    public class PackagePayload
    {
        public decimal payloadId { get; set; }
        public decimal productId { get; set; }
        public decimal payloadAmount { get; set; }
        public decimal sourceMSISDN { get; set; }
        public decimal destinationMSISDN { get; set; }
        public decimal payloadChargeType { get; set; }
        public string apiAuthUsername { get; set; }
        public string apiAuthPassword { get; set; }
        public decimal payloadPackageType { get; set; }
    }

    public class RequestOrderPayload
    {
        public string orderType { get; set; }
        public string sourceMSISDN { get; set; }
        public string destinationMSISDN { get; set; }
        public string productId { get; set; }
        public string payloadId { get; set; }
        public string payloadAmount { get; set; }
        public string payloadChargeType { get; set; }
        public string payloadPackageType { get; set; }
    }

    public class GetPaymentUrlPayload
    {
        public string deepLinkUrl { get; set; }
        public string orderId { get; set; }
    }

    public class GetPaymentUrlResponse
    {
        public string url { get; set; }
        public string responseType { get; set; }
        public string responseDesc { get; set; }
    }

    public class RequestOrderApiGatewayResult
    {
        [JsonProperty("TopupOrderId")]
        public string TopupOrderId { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("ResponseType")]
        public string ResponseType { get; set; }

        [JsonProperty("ResponseDesc")]
        public string ResponseDesc { get; set; }
    }

    //    TopupOrderId

    //TopupOrderId

    //User

    //User

    //ResponseType

    //ResponseType

    //ResponseDesc

    //ResponseDesc

    public class ProductTypeListPayload
    {
        public string Noting { get; set; }
    }

    public class ProductListPayload
    {
        public string productId { get; set; }
    }

    public class BPSRequestModel
    {
        public long amount { get; set; }
        public long orderId { get; set; }
        public long bankId { get; set; }
        public string msisdn { get; set; }
        public string aditionalData { get; set; }
        public long userId { get; set; }
    }

    public class BPSResponseModel
    {
        public bool execStatus { get; set; }
        public long resultCode { get; set; }
        public string resultMessage { get; set; }
        public string data { get; set; }
    }

    public class ConfirmOrderPayload
    {
        public string OrderId { get; set; }
        public string BankCode { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public string RRN { get; set; }
        public string ApigwUser { get; set; }

        //      "orderId": "542",
        //"bankCode": "16",
        //"cardNo": "0079820281",
        //"cardType": "1",
        //"rrn": 0
    }

    public class ConfirmResult
    {
        public bool ExecStatus { get; set; }
        public string ResponseType { get; set; }
        public string ResponseDesc { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }

        //    {
        //  "ExecStatus": false,
        //  "ResponseType": "0",
        //  "ResponseDesc": null
        //}
    }

    public class PaymentStatus
    {
        public decimal payloadId { get; set; }
        public decimal productId { get; set; }
        public decimal payloadAmount { get; set; }
        public decimal sourceMSISDN { get; set; }
        public decimal destinationMSISDN { get; set; }
        public decimal payloadChargeType { get; set; }
        public string apiAuthUsername { get; set; }
        public string apiAuthPassword { get; set; }
        public decimal payloadPackageType { get; set; }
    }

    public class TopupAuth_Request
    {
        public string Username { get; set; }
    }

    public class TopupAuth_Response
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public partial class AppletMapTypeC
    {
        public string authUserName { get; set; }
    }

    public partial class IngredientModel
    {
    }

    public partial class TriggerInstanceKey
    {
        public Guid SdpApi { get; set; }
    }

    public class DbOrderInfoModel
    {
        public long CHANEL_TYPE { get; set; }
        public string FK_USER { get; set; }
        public long ORDER_ID { get; set; }
        public long OFFER_ID { get; set; }
        public long OFFER_TYPE { get; set; }
        public long AMOUNT { get; set; }
        public long TOTAL_AMOUNT { get; set; }
        public long MSISDN { get; set; }
        public long PHONE_NO { get; set; }
        public string INS_DATE { get; set; }
        public long BANK_ID { get; set; }
        public string BANK_USER { get; set; }
        public string BANK_PASS { get; set; }
        public string RETURN_URL { get; set; }
        public string STATUS { get; set; }
        public string TRACK_CODE { get; set; }
        public string ACCOUNT_NO { get; set; }
        public decimal COMMISSION_RATE { get; set; }
        public string MCI_IBAN { get; set; }
    }

    public class OrderInfoSingleModel
    {
        public string Source_Msisdn { get; set; }
        public string Destination_Msisdn { get; set; }
        public string OfferId { get; set; }
        public string Amount { get; set; }
        public string RRN { get; set; }
        public string BankCode { get; set; }
        public string Status { get; set; }
        public string RequestDate { get; set; }
        public string PaymentDate { get; set; }
        //P_TEL_REQUEST => :P_TEL_REQUEST,
        //P_TEL_RECEIVER => :P_TEL_RECEIVER,
        //P_OFFER_ID => :P_OFFER_ID,
        //P_AMOUNT => :P_AMOUNT,
        //P_RRN => :P_RRN,
        //P_BANK_ID => :P_BANK_ID,
        //P_REQUEST_STATUS => :P_REQUEST_STATUS,
        //P_REQUEST_DATE => :P_REQUEST_DATE,
        //P_PAYMENT_DATE => :P_PAYMENT_DATE,
        //P_RESPONSE_TYPE => :P_RESPONSE_TYPE,
        //P_RESPONSE_DESC => :P_RESPONSE_DESC);
    }

    public class StatusOrderModel
    {
        public string orderId { get; set; }
        public string fromTimeStamp { get; set; }
        public string toTimeStamp { get; set; }
    }

    public class AuthModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string TokenUrl { get; set; }
        public string ResumeUrl { get; set; }
    }

    public class TokenResult
    {
        public bool ExecStatus { get; set; }
        public string ResponseType { get; set; }
        public string ResponseDesc { get; set; }

        public string Token { get; set; }
    }

    public class InformedModel
    {
        public string OrderId { get; set; }
    }

    public class GetSamanPaymentUrlPayload
    {
        public string deepLinkUrl { get; set; }
        public string orderId { get; set; }
        public decimal BankId { get; set; }
    }

    public class GetSamanPaymentUrlResponse
    {
        public string url { get; set; }
    }
}