using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging;

public class DiyPriceRequestModel
{
    public decimal Data { get; set; }
    public decimal Sms { get; set; }
    public decimal Voice { get; set; }
}