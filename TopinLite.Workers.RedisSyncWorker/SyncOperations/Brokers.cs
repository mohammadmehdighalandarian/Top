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

    public class Brokers : IBrokers
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
        private readonly ILogger<Brokers> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public Brokers(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
            ILogger<Brokers> logger,
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
            ExecResult<IEnumerable<BrokersModel>> dataFromDb = await _iTopinQueryRepository.GetAllBrokers();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError("Error on Getting Brokers Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<BrokersModel> brokers = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {BrokersCount} Brokers Data Start @ {DateTime} .", brokers.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.Brokers);

            foreach (BrokersModel item in brokers)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.Brokers}{item.SapId}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync Brokers Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}