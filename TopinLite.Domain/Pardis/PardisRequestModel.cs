using System.Text.Json.Serialization;

namespace TopinLite.Domain.Pardis
{
    public class PardisSmsRequestModel
    {
        public PardisSmsRequestModel(List<PardisSingleSmsModel> smsMtDtoList)
        {
            this.smsMtDtoList = smsMtDtoList;
        }

        [JsonPropertyName("smsMtDtoList")]
        public List<PardisSingleSmsModel> smsMtDtoList { get; set; }
    }
    public class PardisSingleSmsModel
    {
        [JsonPropertyName("source")]
        public string source { get; set; }

        [JsonPropertyName("destination")]
        public string destination { get; set; }

        [JsonPropertyName("smsClass")]
        public string smsClass { get => "NORMAL"; }

        [JsonPropertyName("message")]
        public string message { get; set; }
    }
}
