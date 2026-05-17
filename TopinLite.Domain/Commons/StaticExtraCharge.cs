using TopinLite.Domain.TopinApi;

namespace TopinLite.Domain.Commons
{
    public class StaticExtraCharge
    {
        public static readonly IReadOnlyList<ExtraChargeModel> Items = new List<ExtraChargeModel>
        {
            new() { ChargeAmount = 50000, ExtraChargeAmount = 5000, ExpiryTime = 5 },
            new() { ChargeAmount = 100000, ExtraChargeAmount = 15000, ExpiryTime = 7 },
            new() { ChargeAmount = 200000, ExtraChargeAmount = 50000, ExpiryTime = 15  },
            new() { ChargeAmount = 500000, ExtraChargeAmount = 150000, ExpiryTime = 20 },
            new() { ChargeAmount = 1000000, ExtraChargeAmount = 350000, ExpiryTime = 30 }
        };
    }
}
