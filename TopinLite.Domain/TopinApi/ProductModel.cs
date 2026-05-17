using Newtonsoft.Json;

namespace TopinLite.Domain.TopinApi
{
    public class ProductModel : BrokerBaseModel
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("DetailTitle")]
        public string DetailTitle { get; set; }

        [JsonProperty("ChargeType")]
        public string ChargeType { get; set; }

        [JsonProperty("ChargeAmount")]
        public string ChargeAmount { get; set; }

        [JsonProperty("GiftAmount")]
        public string GiftAmount { get; set; }

        [JsonProperty("Amount")]
        public string Amount { get; set; }

        [JsonProperty("ProductId")]
        public string ProductId { get; set; }

        [JsonProperty("ProductTitle")]
        public string ProductTitle { get; set; }

        [JsonProperty("Volume")]
        public string Volume { get; set; }

        [JsonProperty("System")]
        public string System { get; set; }

        [JsonProperty("Duration")]
        public string Duration { get; set; }

        [JsonProperty("AmountWithTax")]
        public string AmountWithTax { get; set; }

        [JsonProperty("ProductCategoryId")]
        public int? ProductCategoryId { get; set; }

        [JsonProperty("ProductCategoryName")]
        public string ProductCategoryName { get; set; }

        [JsonProperty("SubCategoryId")]
        public int? SubCategoryId { get; set; }

        [JsonProperty("SubCategoryName")]
        public string SubCategoryName { get; set; }

        [JsonProperty("DurationUnit")]
        public string DurationUnit { get; set; }

        [JsonProperty("DurationName")]
        public string DurationName { get; set; }

        [JsonProperty("OfferCode")]
        public string OfferCode { get; set; }

        [JsonIgnore]
        public string DIYPropertyStr { get; set; }

        [JsonProperty("DIYProperty")]
        public List<ProductDIYModel> DIYProperty { get; set; }

        [JsonProperty("CategoryId")]
        public string CategoryId { get; set; }

        [JsonProperty("ServiceTypeDesc")]
        public string ServiceTypeDesc { get; set; }

        [JsonProperty("ServiceType")]
        public string ServiceType { get; set; }
    }

    public class ProductDIYModel
    {
        public List<decimal?> volumes { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
    }

    public class ProductDIYViewModel
    {
        public long FK_OFFER_ID { get; set; }
        public long FK_BROKER_ID { get; set; }
        public string DIYPROPERTY { get; set; }
    }
}