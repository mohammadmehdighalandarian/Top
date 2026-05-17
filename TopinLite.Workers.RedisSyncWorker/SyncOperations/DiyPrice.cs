using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface IDiyPrice
    {
        Task<ExecResult> BeginSync();
    }

    public class DiyPrice : IDiyPrice
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _ITopinQueryRepository;
        private readonly ILogger<DiyPrice> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;

        public DiyPrice(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
                        ILogger<DiyPrice> logger,
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
            ExecResult<IEnumerable<DiyPriceModel>> DataFromDb = await _ITopinQueryRepository.GetAllDiyPrices();

            if (!DataFromDb.ExecStatus)
            {
                _logger.LogError($"Error on Getting DiyPrice Data From Database => {DataFromDb.ResultMessage}");
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<DiyPriceModel> list = DataFromDb.Data.ToList();
            _logger.LogInformation($"Sync {list.Count} DiyPrice Data Start @ {DateTime.Now} .");

            var DeleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.DiyPrice);

            foreach (var item in list)
            {
                bool RedisResult = await _redis.SetAsync($"{MemoryDataKeys.DiyPrice}{item.Type}:{item.Unit}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation($"Sync DiyPrice Finished @ {DateTime.Now} .");

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}
