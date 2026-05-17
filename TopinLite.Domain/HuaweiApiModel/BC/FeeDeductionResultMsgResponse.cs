using System.Text.Json.Serialization;

namespace STopinLite.Domain.HuaweiApiModel.BC
{
    public class ResultHeader
    {
        [JsonPropertyName("version")]
        public string version { get; set; }

        [JsonPropertyName("ResultCode")]
        public string ResultCode { get; set; }

        [JsonPropertyName("ResultDesc")]
        public string ResultDesc { get; set; }
    }

    public class BalanceChgInfo
    {
        [JsonPropertyName("BalanceType")]
        public string BalanceType { get; set; }

        [JsonPropertyName("BalanceID")]
        public string BalanceId { get; set; }

        [JsonPropertyName("BalanceTypeName")]
        public string BalanceTypeName { get; set; }

        [JsonPropertyName("OldBalanceAmt")]
        public string OldBalanceAmt { get; set; }

        [JsonPropertyName("NewBalanceAmt")]
        public string NewBalanceAmt { get; set; }

        [JsonPropertyName("CurrencyID")]
        public string CurrencyID { get; set; }

        [JsonPropertyName("CurrExpTime")]
        public string CurrExpTime { get; set; }

        [JsonPropertyName("ChgExpTime")]
        public string ChgExpTime { get; set; }
    }

    public class FeeDeductionResult
    {
        [JsonPropertyName("DeductSerialNo")]
        public string DeductSerialNo { get; set; }

        public AcctBalanceChange AcctBalanceChangeList { get; set; }
    }

    public class AcctBalanceChange
    {
        public string AcctKey { get; set; }
        public BalanceChgInfo BalanceChgInfo { get; set; }
    }

    public class FeeDeductionResultMsg
    {
        [JsonPropertyName("ResultHeader")]
        public ResultHeader ResultHeader { get; set; }

        [JsonPropertyName("FeeDeductionResult")]
        public FeeDeductionResult FeeDeductionResult { get; set; }

        [JsonPropertyName("ReturnCode")]
        public string ReturnCode { get; set; }
    }

    public class Fault
    {
        public string faultcode { get; set; }
        public string faultstring { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("FeeDeductionResultMsg")]
        public FeeDeductionResultMsg FeeDeductionResultMsg { get; set; }

        public Fault Fault { get; set; }
    }

    public class Envelope
    {
        [JsonPropertyName("Body")]
        public Body Body { get; set; }
    }

    public class FeeDeductionResultMsgResult
    {
        [JsonPropertyName("Envelope")]
        public Envelope Envelope { get; set; }
    }

    public class FeeDeductionResultMsgResponse
    {
        [JsonPropertyName("ResponseType")]
        public string ResponseType { get; set; }

        [JsonPropertyName("ResponseDesc")]
        public string ResponseDesc { get; set; }

        public string OldBalanceAmt { get; set; }
        public string NewBalanceAmt { get; set; }
    }
}