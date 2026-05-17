using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface IBrokerOfferAccess
    {
        Task<ExecResult> BeginSync();
    }

    public class BrokerOfferAccess : IBrokerOfferAccess
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _ITopinQueryRepository;
        private readonly ILogger<BrokerOfferAccess> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;

        public BrokerOfferAccess(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
                        ILogger<BrokerOfferAccess> logger,
                        IRedisStringStore redis,
                        IRedisPrefixDeleteService redisPrefixDelete)
        {
            _ITopinQueryRepository = iTopinQueryRepository;
            _logger = logger;
            _redis = redis;
            _redisPrefixDelete = redisPrefixDelete;
        }

        public async Task<ExecResult> BeginSync()
        {
            ExecResult<IEnumerable<BrokerOfferAccessModel>> DataFromDb = await _ITopinQueryRepository.GetAllBrokerOfferAccess();

            if (!DataFromDb.ExecStatus)
            {
                _logger.LogError($"Error on Getting BrokerOfferAccess Data From Database => {DataFromDb.ResultMessage}");
                return new ExecResult { ExecStatus = false };
            }

            List<BrokerOfferAccessModel> list = DataFromDb.Data.ToList();
            _logger.LogInformation($"Sync {list.Count} BrokerOfferAccess Data Start @ {DateTime.Now} .");

            var DeleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.BrokerOfferAccess);

            foreach (var item in list)
            {
                bool RedisResult = await _redis.SetAsync($"{MemoryDataKeys.BrokerOfferAccess}{item.BrokerId}:{item.OfferId}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation($"Sync BrokerOfferAccess Finished @ {DateTime.Now} .");

            return new ExecResult { ExecStatus = true, ResultMessage = "Success", ResultCode = 0 };
        }
    }
}
