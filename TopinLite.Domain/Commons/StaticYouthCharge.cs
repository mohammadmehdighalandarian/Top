using TopinLite.Domain.TopinApi;

namespace TopinLite.Domain.Commons
{
    public class StaticYouthCharge
    {
        public static readonly IReadOnlyList<YouthExtraChargeModel> Items = new List<YouthExtraChargeModel>
        {
            new() { ChargeAmount = 10000, ExpiryTime = 1 },
            new() { ChargeAmount = 20000, ExpiryTime = 2 },
            new() { ChargeAmount = 50000, ExpiryTime = 5  },
            new() { ChargeAmount = 100000, ExpiryTime = 10 },
            new() { ChargeAmount = 200000, ExpiryTime = 20 }
        };
    }
}
