using TopinLite.Domain.TopinApi;

namespace TopinLite.Domain.Commons
{
    public class StaticWomanCharge
    {
        public static readonly IReadOnlyList<WomanChargeExtraModel> Items = new List<WomanChargeExtraModel>
        {
            new() { CardAmount = 10000, GiftAmount = 12037, ExpiryTime = 3 },
            new() { CardAmount = 20000, GiftAmount = 24075, ExpiryTime = 5 },
            new() { CardAmount = 50000, GiftAmount = 60185, ExpiryTime = 10  },
            new() { CardAmount = 100000, GiftAmount = 120371, ExpiryTime = 15 },
            new() { CardAmount = 200000, GiftAmount = 240740, ExpiryTime = 20 }
        };
    }
}