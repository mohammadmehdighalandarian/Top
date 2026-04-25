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


    public interface IBrokersAccessList
    {
        Task<ExecResult> BeginSync();
    }
    public class BrokersAccessList : IBrokersAccessList
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _ITopinQueryRepository;
        private readonly ILogger<BrokersAccessList> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public BrokersAccessList(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
                      ILogger<BrokersAccessList> logger,
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
            ExecResult<IEnumerable<BrokersAccessModel>> DataFromDb = await _ITopinQueryRepository.GetAllBrokersAccess();

            if (!DataFromDb.ExecStatus)
            {
                _logger.LogError($"Error on Getting BrokersAccessList Data From Database => {DataFromDb.ResultMessage}");
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<BrokersAccessModel> OfferList = DataFromDb.Data.ToList();
            _logger.LogInformation($"Sync {OfferList.Count} BrokersAccessList Data Start @ {DateTime.Now} .");

            var DeleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.BrokersAccess);

            foreach (var item in OfferList)
            {
                bool RedisResult = await _redis.SetAsync($"{MemoryDataKeys.BrokersAccess}{item.SapId}:{item.MethodName}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation($"Sync BrokersAccessList Finished @ {DateTime.Now} .");

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}
