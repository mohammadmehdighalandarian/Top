namespace TopinLite.Infrastructure.CacheData.ServiceProviders
{
    public interface IInMemoryDataProvider
    {
        Task<BrokersModel> GetBrokerInfoBySapId(int SapId);
        Task<BrokersAccessModel> GetBrokersAccessBySapIdAndMethodName(long SapId, string MethodName);
        Task<DynamicConditionModel> GetDynamicConditionsByParameters(long DynamicConditionId, string Biztype, string KeyStr);
        Task<OffersModel> GetOfferInfoByOfferId(int OfferId);
    }

    public class InMemoryDataProvider : IInMemoryDataProvider
    {
        private readonly IRedisStringStore _IRedisStringStore;

        public InMemoryDataProvider(IRedisStringStore redis)
        {
            _IRedisStringStore = redis;
        }

        public async Task<OffersModel> GetOfferInfoByOfferId(int OfferId)
        {
            string? CacheResult = await _IRedisStringStore.GetAsync($"{MemoryDataKeys.Offers}{OfferId}");

            if (CacheResult is not null)
            {
                return JsonSerializer.Deserialize<OffersModel>(CacheResult);
            }

            return null;
        }

        public async Task<BrokersModel> GetBrokerInfoBySapId(int SapId)
        {
            string? CacheResult = await _IRedisStringStore.GetAsync($"{MemoryDataKeys.Brokers}{SapId}");

            if (CacheResult is not null)
            {
                return JsonSerializer.Deserialize<BrokersModel>(CacheResult);
            }

            return null;
        }

        public async Task<BrokersAccessModel> GetBrokersAccessBySapIdAndMethodName(Int64 SapId,string MethodName)
        {
            string? CacheResult = await _IRedisStringStore.GetAsync($"{MemoryDataKeys.BrokersAccess}{SapId}:{MethodName}");

            if (CacheResult is not null)
            {
                return JsonSerializer.Deserialize<BrokersAccessModel>(CacheResult);
            }

            return null;
        }

        public async Task<DynamicConditionModel> GetDynamicConditionsByParameters(Int64 DynamicConditionId,string Biztype,string KeyStr)
        {
            string? CacheResult = await _IRedisStringStore.GetAsync($"{MemoryDataKeys.DynamicCondition}{DynamicConditionId}*{Biztype}*{KeyStr}");

            if (CacheResult is not null)
            {
                return JsonSerializer.Deserialize<DynamicConditionModel>(CacheResult);
            }

            return null;
        }
    }
}