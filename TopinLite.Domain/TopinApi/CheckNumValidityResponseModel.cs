using Newtonsoft.Json;

namespace TopinLite.Domain.TopinApi
{
    public class CheckNumValidityResponseModel
    {
        [JsonProperty("ExecStatus")]
        public bool ExecStatus { get; set; }

        [JsonProperty("ResponseType")]
        [JsonConverter(typeof(ResultCodeStringConverter<decimal>))]
        public decimal ResultCode { get; set; }

        [JsonProperty("ResponseDesc")]
        public string ResultMessage { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}