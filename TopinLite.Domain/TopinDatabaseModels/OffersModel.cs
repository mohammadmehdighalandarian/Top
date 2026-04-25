namespace TopinLite.Domain.TopinDatabaseModels
{
    public class OffersModel
    {
        public decimal OfferId { get; set; }
        public decimal OfferCode { get; set; }
        public string OfferName { get; set; }
        public decimal Type { get; set; }
        public string Type_Desc { get; set; }
        public decimal Category { get; set; }
        public string CategoryDesc { get; set; }
        public decimal Duration { get; set; }
        public decimal Price { get; set; }
        public decimal SystemId { get; set; }
        public string SystemType { get; set; }
        public string StatusTitle { get; set; }
        public decimal Status { get; set; }
        public decimal RelationId { get; set; }
        public string RelationName { get; set; }
        public decimal PackageVolume { get; set; }
    }
}