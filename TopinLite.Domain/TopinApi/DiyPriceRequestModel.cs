using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.TopinApi;

public class DiyPriceResponseModel : DiyPriceModel
{
}
public class DiyPriceRequestModel
{
    public decimal Sms { get; set; }
    public decimal Voice { get; set; }
    public decimal Data { get; set; }
}


