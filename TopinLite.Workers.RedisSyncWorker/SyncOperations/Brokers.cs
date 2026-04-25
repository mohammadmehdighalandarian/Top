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
    public interface IBrokers
    {
        Task<ExecResult> BeginSync();
    }
    public class Brokers: IBrokers
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _ITopinQueryRepository;
        private readonly ILogger<Brokers> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;
        

        public Brokers(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
                      ILogger<Brokers> logger,
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
            ExecResult<IEnumerable<BrokersModel>> DataFromDb = await _ITopinQueryRepository.GetAllBrokers();

            if (!DataFromDb.ExecStatus)
            {
                _logger.LogError($"Error on Getting Brokers Data From Database => {DataFromDb.ResultMessage}");
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<BrokersModel> OfferList = DataFromDb.Data.ToList();
            _logger.LogInformation($"Sync {OfferList.Count} Brokers Data Start @ {DateTime.Now} .");

            var DeleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.Brokers);

            foreach (var item in OfferList)
            {
                bool RedisResult = await _redis.SetAsync($"{MemoryDataKeys.Brokers}{item.SapId}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation($"Sync Brokers Finished @ {DateTime.Now} .");

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}
