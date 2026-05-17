using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface ISelectedBrokers
    {
        Task<ExecResult> BeginSync();
    }

    public class SelectedBrokers : ISelectedBrokers
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
        private readonly ILogger<SelectedBrokers> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public SelectedBrokers(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
            ILogger<SelectedBrokers> logger,
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
            ExecResult<IEnumerable<SelectedBrokerModel>> dataFromDb = await _iTopinQueryRepository.GetSelectedBrokers();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError("Error on Getting SelectedBrokers Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<SelectedBrokerModel> selectedBrokers = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {SelectedBrokersCount} SelectedBrokers Data Start @ {DateTime} .", selectedBrokers.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.SelectedBrokers);

            foreach (SelectedBrokerModel item in selectedBrokers)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.SelectedBrokers}{item.BrokerId}:{item.OfferId}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync SelectedBrokers Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}
