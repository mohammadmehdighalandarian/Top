namespace TopinLite.Domain.TopinDatabaseModels;

public class RuleAccountModel
{
    public decimal RuleId { get; set; }
    public decimal BrokerId { get; set; }
    public long AccountId { get; set; }
    public string AccountName { get; set; }
    public decimal Credit { get; set; }
    public decimal DailyLimitation { get; set; }
    public decimal MonthlyLimitation { get; set; }
    public decimal ExpiredCredit { get; set; }

}

