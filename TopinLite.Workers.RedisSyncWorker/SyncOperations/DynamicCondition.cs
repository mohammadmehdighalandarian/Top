using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface IDynamicCondition
    {
        Task<ExecResult> BeginSync();
    }

    public class DynamicCondition : IDynamicCondition
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
        private readonly ILogger<DynamicCondition> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public DynamicCondition(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
            ILogger<DynamicCondition> logger,
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
            ExecResult<IEnumerable<DynamicConditionModel>> dataFromDb = await _iTopinQueryRepository.GetAllDynamicCondition();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError("Error on Getting DynamicCondition Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<DynamicConditionModel> dynamicConditionList = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {dynamicConditionList} DynamicCondition Data Start @ {dateTime} .", dynamicConditionList.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.DynamicCondition);

            foreach (DynamicConditionModel item in dynamicConditionList)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.DynamicCondition}{item.Biztype}*{item.KeyStr}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync DynamicCondition Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}