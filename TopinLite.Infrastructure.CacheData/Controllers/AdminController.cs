namespace TopinLite.Infrastructure.CacheData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ServiceProviders.IInMemoryDataProvider _memoryService;

        public AdminController(ServiceProviders.IInMemoryDataProvider memoryService)
        {
            _memoryService = memoryService;
        }

        [HttpGet("Offers")]
        public async Task<OffersModel> GetOfferById([FromQuery] int id, CancellationToken cancellationToken)
        {
            OffersModel dataResult = await _memoryService.GetOfferInfoByOfferId(id);

            if (dataResult is not null)
            {
                return dataResult;
            }

            return new OffersModel();
        }

        [HttpGet("Brokers")]
        public async Task<BrokersModel> GetBrokersBySapId([FromQuery] int id, CancellationToken cancellationToken)
        {
            BrokersModel dataResult = await _memoryService.GetBrokerInfoBySapId(id);

            if (dataResult is not null)
            {
                return dataResult;
            }

            return new BrokersModel();
        }

        [HttpGet("BrokersAccess")]
        public async Task<BrokersAccessModel> GetBrokersAccessBySapIdAndMethodName([FromQuery] Int64 id, [FromQuery] string methodName,
            CancellationToken cancellationToken)
        {
            BrokersAccessModel dataResult = await _memoryService.GetBrokersAccessBySapIdAndMethodName(id, methodName);

            if (dataResult is not null)
            {
                return dataResult;
            }

            return new BrokersAccessModel();
        }


        [HttpGet("DynamicConditions")]
        public async Task<DynamicConditionModel> GetDynamicConditions([FromQuery] string bizType, [FromQuery] string keyStr, CancellationToken cancellationToken)
        {
            DynamicConditionModel dataResult = await _memoryService.GetDynamicConditionsByParameters(bizType, keyStr);

            if (dataResult is not null)
            {
                return dataResult;
            }

            return new DynamicConditionModel();
        }

        [HttpGet("DiyPrice")]
        public async Task<decimal> GetDiyPrice([FromQuery] decimal data, [FromQuery] decimal voice, [FromQuery] decimal sms, CancellationToken cancellationToken)
        {
            decimal dataResult = await _memoryService.GetDiyPriceByUnits(data, voice, sms);
            return dataResult;
        }

        [HttpGet("GetBrokerAccessByOfferAndBrokerId")]
        public async Task<BrokerOfferAccessModel> GetBrokerAccessByOfferAndBrokerId([FromQuery] long brokerId, [FromQuery] long offerId,
            CancellationToken cancellationToken)
        {
            BrokerOfferAccessModel dataResult = await _memoryService.GetBrokerAccessByOfferAndBrokerId(brokerId, offerId);
            return dataResult;
        }

        [HttpGet("GetDiyDataAccess")]
        public async Task<int> GetDiyDataAccess([FromQuery] long brokerId, [FromQuery] long offerId, [FromQuery] decimal data, CancellationToken cancellationToken)
        {
            int dataResult = await _memoryService.GetDiyDataAccess(brokerId, offerId, data);
            return dataResult;
        }
        
        [HttpGet("PrimaryOffers")]
        public async Task<List<PrimaryOffersModel>> GetPrimaryOffers(CancellationToken cancellationToken)
        {
            List<PrimaryOffersModel> dataResult = await _memoryService.GetPrimaryOffers();

            if (dataResult.Count > 0)
            {
                return dataResult;
            }

            return [];
        }
        
        [HttpGet("BrokersSaleLimitByBrokerId")]
        public async Task<BrokerSaleLimitModel> GetBrokerSaleLimitByBrokerId([FromQuery] decimal brokerId, CancellationToken ct)
        {
            BrokerSaleLimitModel dataResult = await _memoryService.GetBrokerSaleLimitByBrokerId(brokerId);

            if (dataResult is not null)
            {
                return dataResult;
            }

            return new BrokerSaleLimitModel();
        }

        [HttpGet("GetTradeTypeByRechargeTypeAndOperatorId")]
        public async Task<TradeTypeModel> GetTradeTypeByRechargeTypeAndOperatorId([FromQuery] decimal rechargeType, [FromQuery] decimal operatorId, CancellationToken ct)
        {
            TradeTypeModel dataResult = await _memoryService.GetTradeTypeByRechargeTypeAndOperationId(rechargeType, operatorId);

            if (dataResult is not null)
            {
                return dataResult;
            }

            return new TradeTypeModel();
        }

        [HttpGet("GetSelectedBrokerByBrokerIdAndOfferId")]
        public async Task<SelectedBrokerModel> GetSelectedBrokerByBrokerIdAndOfferId([FromQuery] decimal brokerId, [FromQuery] decimal offerId, CancellationToken ct)
        {
            SelectedBrokerModel dataResult = await _memoryService.GetSelectedBrokerByBrokerIdAndOfferId(brokerId, offerId);

            if (dataResult is not null)
            {
                return dataResult;
            }

            return new SelectedBrokerModel();
        }
    }
}