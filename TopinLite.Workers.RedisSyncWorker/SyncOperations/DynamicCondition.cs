using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _ITopinQueryRepository;
        private readonly ILogger<DynamicCondition> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public DynamicCondition(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
                      ILogger<DynamicCondition> logger,
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
            ExecResult<IEnumerable<DynamicConditionModel>> DataFromDb = await _ITopinQueryRepository.GetAllDynamicCondition();

            if (!DataFromDb.ExecStatus)
            {
                _logger.LogError($"Error on Getting DynamicCondition Data From Database => {DataFromDb.ResultMessage}");
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<DynamicConditionModel> OfferList = DataFromDb.Data.ToList();
            _logger.LogInformation($"Sync {OfferList.Count} DynamicCondition Data Start @ {DateTime.Now} .");

            var DeleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.DynamicCondition);

            foreach (var item in OfferList)
            {
                bool RedisResult = await _redis.SetAsync($"{MemoryDataKeys.DynamicCondition}{item.DynamicConditionId}*{item.Biztype}*{item.KeyStr}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation($"Sync DynamicCondition Finished @ {DateTime.Now} .");

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}
