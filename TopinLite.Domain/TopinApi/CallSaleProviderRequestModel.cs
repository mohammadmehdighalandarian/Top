using Newtonsoft.Json;

namespace TopinLite.Domain.TopinApi
{
    public class CallSaleProviderRequestModel
    {
        [JsonProperty("TelNum")]
        public string TelNum { get; set; }

        [JsonProperty("TelCharger")]
        public string TelCharger { get; set; }

        [JsonProperty("Amount")]
        public string Amount { get; set; }

        [JsonProperty("ChargeType")]
        public string ChargeType { get; set; }

        //[JsonProperty("BrokerId")]
        //public string BrokerId { get; set; }

        [JsonProperty("ChannelId")]
        public string ChannelId { get; set; }
    }
}