using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public class TradeTypeResponseModel : TradeTypeModel
    {
    }

    public class TradeTypeRequestModel
    {
        public decimal RechargeType { get; set; }
        public decimal OperatorId { get; set; }
    }
}
