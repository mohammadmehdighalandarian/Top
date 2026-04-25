using System.Text.Json.Serialization;

namespace TopinLite.Domain.HuaweiApiModel.AR
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class ResultHeader
    {
        [JsonPropertyName("Version")]
        public string Version { get; set; }

        [JsonPropertyName("ResultCode")]
        public string ResultCode { get; set; }

        [JsonPropertyName("MsgLanguageCode")]
        public string MsgLanguageCode { get; set; }

        [JsonPropertyName("ResultDesc")]
        public string ResultDesc { get; set; }
    }

    public class BalanceChgInfo
    {
        [JsonPropertyName("BalanceType")]
        public string BalanceType { get; set; }

        [JsonPropertyName("BalanceID")]
        public string BalanceID { get; set; }

        [JsonPropertyName("BalanceTypeName")]
        public string BalanceTypeName { get; set; }

        [JsonPropertyName("OldBalanceAmt")]
        public string OldBalanceAmt { get; set; }

        [JsonPropertyName("NewBalanceAmt")]
        public string NewBalanceAmt { get; set; }

        [JsonPropertyName("CurrencyID")]
        public string CurrencyID { get; set; }
    }

    public class OldLifeCycleStatu
    {
        [JsonPropertyName("StatusName")]
        public string StatusName { get; set; }

        [JsonPropertyName("StatusExpireTime")]
        public string StatusExpireTime { get; set; }

        [JsonPropertyName("StatusIndex")]
        public string StatusIndex { get; set; }
    }

    public class NewLifeCycleStatu
    {
        [JsonPropertyName("StatusName")]
        public string StatusName { get; set; }

        [JsonPropertyName("StatusExpireTime")]
        public string StatusExpireTime { get; set; }

        [JsonPropertyName("StatusIndex")]
        public string StatusIndex { get; set; }
    }

    public class LifeCycleChgInfo
    {
        [JsonPropertyName("OldLifeCycleStatus")]
        public List<OldLifeCycleStatu> OldLifeCycleStatus { get; set; }

        [JsonPropertyName("NewLifeCycleStatus")]
        public List<NewLifeCycleStatu> NewLifeCycleStatus { get; set; }

        [JsonPropertyName("AddValidity")]
        public string AddValidity { get; set; }
    }

    public class RechargeResult
    {
        [JsonPropertyName("RechargeSerialNo")]
        public string RechargeSerialNo { get; set; }

        [JsonPropertyName("BalanceChgInfo")]
        public BalanceChgInfo BalanceChgInfo { get; set; }

        [JsonPropertyName("LifeCycleChgInfo")]
        public LifeCycleChgInfo LifeCycleChgInfo { get; set; }
    }

    public class RechargeResultMsg
    {
        [JsonPropertyName("ResultHeader")]
        public ResultHeader ResultHeader { get; set; }

        [JsonPropertyName("RechargeResult")]
        public RechargeResult RechargeResult { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("RechargeResultMsg")]
        public RechargeResultMsg RechargeResultMsg { get; set; }
    }

    public class Envelope
    {
        [JsonPropertyName("Body")]
        public Body Body { get; set; }
    }

    public class RechargeResponseModel
    {
        [JsonPropertyName("Envelope")]
        public Envelope Envelope { get; set; }
    }
}