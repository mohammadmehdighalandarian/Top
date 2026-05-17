using System.Text.Json.Serialization;

namespace TopinLite.Domain.HuaweiApiModel.Eligibility
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class ResultHeader
    {
        [JsonPropertyName("version")]
        public string version { get; set; }

        [JsonPropertyName("resultCode")]
        public string resultCode { get; set; }

        [JsonPropertyName("MsgLanguageCode")]
        public string MsgLanguageCode { get; set; }

        [JsonPropertyName("resultDesc")]
        public string resultDesc { get; set; }
    }

    public class CheckOfferingEligibilityRspMsg
    {
        [JsonPropertyName("resultHeader")]
        public ResultHeader resultHeader { get; set; }

        //[JsonPropertyName("RechargeResult")]
        //public RechargeResult RechargeResult { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("CheckOfferingEligibilityRspMsg")]
        public CheckOfferingEligibilityRspMsg CheckOfferingEligibilityRspMsg { get; set; }

        public Fault Fault { get; set; }
    }

    public class Fault
    {
        public string faultcode { get; set; }
        public string faultstring { get; set; }
    }

    public class Envelope
    {
        [JsonPropertyName("Body")]
        public Body Body { get; set; }
    }

    public class CheckOfferingEligibilityRspMsgResponseModel
    {
        [JsonPropertyName("Envelope")]
        public Envelope Envelope { get; set; }
    }
}