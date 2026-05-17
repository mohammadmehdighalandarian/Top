using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface IBrokersAccessList
    {
        Task<ExecResult> BeginSync();
    }

    public class BrokersAccessList : IBrokersAccessList
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
        private readonly ILogger<BrokersAccessList> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public BrokersAccessList(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
            ILogger<BrokersAccessList> logger,
            IRedisStringStore redis,
            IRedisPrefixDeleteService redisPrefixDelete)
        {
            _iTopinQueryRepository = iTopinQueryRepository;
            _logger = logger;
            _redis = redis;
            _redisPrefixDelete = redisPrefixDelete;
        }

        public async Task<ExecResult> BeginSync()
        {
            ExecResult<IEnumerable<BrokersAccessModel>> dataFromDb = await _iTopinQueryRepository.GetAllBrokersAccess();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError("Error on Getting BrokersAccessList Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<BrokersAccessModel> brokerAccessList = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {OfferListCount} BrokersAccessList Data Start @ {DateTime} .", brokerAccessList.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.BrokersAccess);

            foreach (BrokersAccessModel item in brokerAccessList)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.BrokersAccess}{item.SapId}:{item.MethodName}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync BrokersAccessList Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}