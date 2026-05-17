using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.infra.OracleDataAccess.Queries
{
    public interface ITopinQueryRepository
    {
        Task<ExecResult<IEnumerable<BrokersModel>>> GetAllBrokers();
        Task<ExecResult<IEnumerable<BrokersAccessModel>>> GetAllBrokersAccess();
        Task<ExecResult<IEnumerable<DynamicConditionModel>>> GetAllDynamicCondition();
        Task<ExecResult<IEnumerable<OffersModel>>> GetAllOffers();
        Task<ExecResult<IEnumerable<DiyPriceModel>>> GetAllDiyPrices();
        Task<ExecResult<IEnumerable<BrokerOfferAccessModel>>> GetAllBrokerOfferAccess();
        Task<ExecResult<IEnumerable<DiyDataAccessModel>>> GetAllDiyDataAccess();
        Task<ExecResult<IEnumerable<RuleAccountModel>>> GetAllRuleAccounts();
        Task<ExecResult<IEnumerable<PrimaryOffersModel>>> GetAllPrimaryOffers();
        Task<ExecResult<IEnumerable<BrokerSaleLimitModel>>> GetBrokerSaleLimits();
        Task<ExecResult<IEnumerable<TradeTypeModel>>> GetTradeTypes();
        Task<ExecResult<IEnumerable<SelectedBrokerModel>>> GetSelectedBrokers();
        Task<ExecResult<IEnumerable<BrokerSmsModel>>> GetBrokerSmsTexts();
        Task<ExecResult<IEnumerable<MessagesModel>>> GetMessages();
    }
}
