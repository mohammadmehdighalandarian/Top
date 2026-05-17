using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Infrastructure.CacheData.ServiceProviders
{
    public interface IInMemoryDataProvider
    {
        Task<BrokersModel> GetBrokerInfoBySapId(decimal SapId);
        Task<BrokersAccessModel> GetBrokersAccessBySapIdAndMethodName(long SapId, string MethodName);
        Task<DynamicConditionModel> GetDynamicConditionsByParameters(string Biztype, string KeyStr);
        Task<OffersModel> GetOfferInfoByOfferId(decimal offerCode);
        Task<decimal> GetDiyPriceByUnits(decimal data, decimal voice, decimal sms);
        Task<BrokerOfferAccessModel> GetBrokerAccessByOfferAndBrokerId(decimal brokerId, decimal offerId);
        Task<int> GetDiyDataAccess(decimal brokerId, decimal offerId, decimal data);
        Task<List<PrimaryOffersModel>> GetPrimaryOffers();
        Task<BrokerSaleLimitModel> GetBrokerSaleLimitByBrokerId(decimal brokerId);
        Task<SelectedBrokerModel> GetSelectedBrokerByBrokerIdAndOfferId(decimal brokerId, decimal offerId);
        Task<TradeTypeModel> GetTradeTypeByRechargeTypeAndOperationId(decimal rechargeType, decimal operatorId);
        Task<BrokerSmsModel> GetBrokerSmsTextByBrokerIdAndRequestTypeAndSmsId(decimal smsId, decimal brokerId, decimal requestType);
        Task<MessagesModel> GetMessageByIdAndMethod(decimal messageId, string methodName);
    }

    public class InMemoryDataProvider : IInMemoryDataProvider
    {
        private readonly IRedisStringStore _iRedisStringStore;

        public InMemoryDataProvider(IRedisStringStore redis)
        {
            _iRedisStringStore = redis;
        }

        public async Task<OffersModel> GetOfferInfoByOfferId(decimal offerCode)
        {
            string? CacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.Offers}{offerCode}");

            if (CacheResult is not null)
            {
                return JsonSerializer.Deserialize<OffersModel>(CacheResult);
            }

            return null;
        }

        public async Task<BrokersModel> GetBrokerInfoBySapId(decimal SapId)
        {
            string? CacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.Brokers}{SapId}");

            if (CacheResult is not null)
            {
                return JsonSerializer.Deserialize<BrokersModel>(CacheResult);
            }

            return null;
        }

        public async Task<BrokersAccessModel> GetBrokersAccessBySapIdAndMethodName(Int64 SapId, string MethodName)
        {
            string? CacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.BrokersAccess}{SapId}:{MethodName}");

            if (CacheResult is not null)
            {
                return JsonSerializer.Deserialize<BrokersAccessModel>(CacheResult);
            }

            return null;
        }

        public async Task<DynamicConditionModel> GetDynamicConditionsByParameters(string Biztype, string KeyStr)
        {
            string? CacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.DynamicCondition}{Biztype}*{KeyStr}");

            if (CacheResult is not null)
            {
                return JsonSerializer.Deserialize<DynamicConditionModel>(CacheResult);
            }

            return null;
        }

        public async Task<decimal> GetDiyPriceByUnits(decimal data, decimal voice, decimal sms)
        {
            decimal total = 0;
            var filters = new List<(decimal type, decimal unit)>
            {
                (1, data),
                (2, voice),
                (3, sms)
            };

            foreach (var filter in filters)
            {
                string? cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.DiyPrice}{filter.type}:{filter.unit}");
                if (cacheResult is not null)
                {
                    var priceItem = JsonSerializer.Deserialize<DiyPriceModel>(cacheResult);
                    if (priceItem is not null)
                    {
                        total += priceItem.Price;
                    }
                }
            }

            return total;
        }

        public async Task<BrokerOfferAccessModel> GetBrokerAccessByOfferAndBrokerId(decimal brokerId, decimal offerId)
        {
            string? cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.BrokerOfferAccess}{brokerId}:{offerId}");
            if (cacheResult is not null)
            {
                return JsonSerializer.Deserialize<BrokerOfferAccessModel>(cacheResult);
            }

            return new BrokerOfferAccessModel
            {
                BrokerId = brokerId,
                OfferId = offerId,
                Status = 0
            };
        }

        public async Task<int> GetDiyDataAccess(decimal brokerId, decimal offerId, decimal data)
        {
            string? cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.DiyDataAccess}{brokerId}:{offerId}");
            if (cacheResult is null)
            {
                return 0;
            }

            var items = JsonSerializer.Deserialize<List<DiyDataAccessModel>>(cacheResult);
            if (items is null)
            {
                return 0;
            }

            return items.Any(x => data >= x.AttributeMin && data <= x.AttributeMax) ? 1 : 0;
        }

        public async Task<List<PrimaryOffersModel>> GetPrimaryOffers()
        {
            string? cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.PrimaryOffers}");

            if (cacheResult is not null)
            {
                return JsonSerializer.Deserialize<List<PrimaryOffersModel>>(cacheResult);
            }

            return null;
        }

        public async Task<BrokerSaleLimitModel> GetBrokerSaleLimitByBrokerId(decimal brokerId)
        {
            string? cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.BrokerSaleLimits}{brokerId}");
            if (cacheResult is not null)
            {
                return JsonSerializer.Deserialize<BrokerSaleLimitModel>(cacheResult);
            }

            return null;
        }

        public async Task<SelectedBrokerModel> GetSelectedBrokerByBrokerIdAndOfferId(decimal brokerId, decimal offerId)
        {
            string? cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.SelectedBrokers}{brokerId}:{offerId}");
            if (cacheResult is not null)
            {
                return JsonSerializer.Deserialize<SelectedBrokerModel>(cacheResult);
            }

            return null;
        }

        public async Task<TradeTypeModel> GetTradeTypeByRechargeTypeAndOperationId(decimal rechargeType, decimal operatorId)
        {
            string? cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.TradeTypes}{rechargeType}:{operatorId}");
            if (cacheResult is not null)
            {
                return JsonSerializer.Deserialize<TradeTypeModel>(cacheResult);
            }

            return null;
        }

        public async Task<BrokerSmsModel> GetBrokerSmsTextByBrokerIdAndRequestTypeAndSmsId(decimal smsId, decimal brokerId, decimal requestType)
        {
            string? cacheResult = null;

            if (smsId == 0)
                cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.BrokerSmsTexts}{brokerId}:{requestType}");
            else
                cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.BrokerSmsTexts}{smsId}");
            if (cacheResult is not null)
            {
                return JsonSerializer.Deserialize<BrokerSmsModel>(cacheResult);
            }

            return null;
        }

        public async Task<MessagesModel> GetMessageByIdAndMethod(decimal messageId, string methodName)
        {
             string? cacheResult = await _iRedisStringStore.GetAsync($"{MemoryDataKeys.Messages}{messageId}:{methodName}");

            if (cacheResult is not null)
            {
                return JsonSerializer.Deserialize<MessagesModel>(cacheResult);
            }

            return null;
        }
    }
}