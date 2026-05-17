using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.TopinApi;

public class RuleAccountResponseModel
{
    public List<RuleAccountModel> Accounts { get; set; }
}
public class RuleAccountRequestModel
{
    public decimal RuleId { get; set; }
    public decimal BrokerId { get; set; }

}
