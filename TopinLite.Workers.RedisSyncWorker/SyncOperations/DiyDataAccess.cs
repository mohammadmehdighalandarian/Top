using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface IDiyDataAccess
    {
        Task<ExecResult> BeginSync();
    }

    public class DiyDataAccess : IDiyDataAccess
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _ITopinQueryRepository;
        private readonly ILogger<DiyDataAccess> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;

        public DiyDataAccess(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
                             ILogger<DiyDataAccess> logger,
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
            ExecResult<IEnumerable<DiyDataAccessModel>> DataFromDb = await _ITopinQueryRepository.GetAllDiyDataAccess();
            if (!DataFromDb.ExecStatus)
            {
                _logger.LogError($"Error on Getting DiyDataAccess Data From Database => {DataFromDb.ResultMessage}");
                return new ExecResult { ExecStatus = false };
            }

            List<DiyDataAccessModel> list = DataFromDb.Data.ToList();
            _logger.LogInformation($"Sync {list.Count} DiyDataAccess Data Start @ {DateTime.Now} .");

            await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.DiyDataAccess);

            foreach (var group in list.GroupBy(x => new { x.BrokerId, x.OfferId }))
            {
                var key = $"{MemoryDataKeys.DiyDataAccess}{group.Key.BrokerId}:{group.Key.OfferId}";
                bool RedisResult = await _redis.SetAsync(key, JsonSerializer.Serialize(group.ToList()));
            }

            _logger.LogInformation($"Sync DiyDataAccess Finished @ {DateTime.Now} .");
            return new ExecResult { ExecStatus = true, ResultMessage = "Success", ResultCode = 0 };
        }
    }
}
