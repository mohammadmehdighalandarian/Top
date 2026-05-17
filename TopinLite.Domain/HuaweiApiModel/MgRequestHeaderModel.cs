using System.Text.Json.Serialization;

namespace TopinLite.Domain.HuaweiApiModel
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class SessionEntity
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; }

        [JsonPropertyName("RemoteAddress")]
        public string RemoteAddress { get; set; }
    }

    public class MgRequestHeaderModel
    {
        [JsonPropertyName("CommandId")]
        public string CommandId { get; set; }

        [JsonPropertyName("Version")]
        public string Version { get; set; }

        [JsonPropertyName("TransactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("SequenceId")]
        public string SequenceId { get; set; }

        [JsonPropertyName("RequestType")]
        public string RequestType { get; set; }

        [JsonPropertyName("SessionEntity")]
        public SessionEntity SessionEntity { get; set; }

        [JsonPropertyName("OperatorID")]
        public string OperatorID { get; set; }

        [JsonPropertyName("SerialNo")]
        public string SerialNo { get; set; }

        [JsonPropertyName("Channel")]
        public string Channel { get; set; }
    }
}