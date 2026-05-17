using Newtonsoft.Json;

namespace TopinLite.Domain.TopinApi
{
    public class ExecResult<T>
    {
        [JsonProperty("ExecStatus")]
        public bool ExecStatus { get; set; }

        [JsonProperty("ResponseType")]
        [JsonConverter(typeof(ResultCodeStringConverter<decimal>))]
        public decimal ResultCode { get; set; }

        [JsonProperty("ResponseDesc")]
        public string ResultMessage { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }

    public class ExecResultApiG<T>
    {
        [JsonProperty("data")]
        public T data { get; set; }
    }

    public class ExecResult
    {
        [JsonProperty("ExecStatus")]
        public bool ExecStatus { get; set; }

        [JsonProperty("ResponseType")]
        [JsonConverter(typeof(ResultCodeStringConverter<decimal>))]
        public decimal ResultCode { get; set; }

        [JsonProperty("ResponseDesc")]
        public string ResultMessage { get; set; }
    }

    public class ResultCodeStringConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return decimal.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}