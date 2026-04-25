namespace TopinLite.Domain.TopinApi
{
    public class GetHamrahiPackageInfoRes
    {
        public string ResponseType { get; set; }
        public string ResponseDesc { get; set; }
        public string[] Offers { get; set; }
        public string[] Prices { get; set; }
    }

    public class GetHamrahiPackageInfoWithCategoryRes
    {
        public string ResponseType { get; set; }
        public string ResponseDesc { get; set; }
        public string Count { get; set; }
        public object Data { get; set; }
    }

    public class GetHamrahiPackageInfoResV2<T>
    {
        public string resultCode { get; set; }
        public string resultDesc { get; set; }
        public int count { get; set; }
        public List<HamrahiDetails> data { get; set; }
    }

    public class HamrahiDetails
    {
        public int? offerId { get; set; }
        public string offerName { get; set; }
        public string shortName { get; set; }
        public string confirmText { get; set; }
        public int? price { get; set; }
        public int? reservedOfferId { get; set; }
        public string category { get; set; }
    }
}