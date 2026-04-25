using Newtonsoft.Json;

namespace TopinLite.Domain.TopinApi
{
    public class CallSaleProviderPackageRequestModel
    {
        [JsonProperty("TelNum")]
        public string TelNum { get; set; }

        [JsonProperty("TelCharger")]
        public string TelCharger { get; set; }

        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        [JsonProperty("PackageType")]
        public string PackageType { get; set; }

        //[JsonProperty("BrokerId")]
        //public string BrokerId { get; set; }

        [JsonProperty("ChannelId")]
        public string ChannelId { get; set; }

        [JsonProperty("Sms")]
        public string Sms { get; set; }

        [JsonProperty("Voice")]
        public string Voice { get; set; }

        [JsonProperty("Gprs")]
        public string Gprs { get; set; }
    }
}