using Newtonsoft.Json;

namespace TopinLite.Domain.TopinApi
{
    public class CheckBrokerValidityResponseModel
    {
        [JsonProperty("ExecStatus")]
        public bool ExecStatus { get; set; }

        [JsonProperty("ResponseType")]
        [JsonConverter(typeof(ResultCodeStringConverter<decimal>))]
        public decimal ResultCode { get; set; }

        [JsonProperty("ResponseDesc")]
        public string ResultMessage { get; set; }

        [JsonProperty("OrderId")]
        public string OrderId { get; set; }
    }
}