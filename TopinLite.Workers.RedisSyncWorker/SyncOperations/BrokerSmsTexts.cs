using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface IBrokerSmsTexts
    {
        Task<ExecResult> BeginSync();
        Task<ExecResult> BeginSyncBySmsId();
    }

    public class BrokerSmsTexts : IBrokerSmsTexts
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
        private readonly ILogger<BrokerSmsTexts> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public BrokerSmsTexts(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
            ILogger<BrokerSmsTexts> logger,
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
            ExecResult<IEnumerable<BrokerSmsModel>> dataFromDb = await _iTopinQueryRepository.GetBrokerSmsTexts();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError("Error on Getting BrokerSmsTexts Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<BrokerSmsModel> brokerSmsTexts = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {SelectedBrokersCount} BrokerSmsTexts Data Start @ {DateTime} .", brokerSmsTexts.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.BrokerSmsTexts);

            foreach (BrokerSmsModel item in brokerSmsTexts)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.BrokerSmsTexts}{item.BrokerId}:{item.RequestType}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync BrokerSmsTexts Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }

        public async Task<ExecResult> BeginSyncBySmsId()
        {
            ExecResult<IEnumerable<BrokerSmsModel>> dataFromDb = await _iTopinQueryRepository.GetBrokerSmsTexts();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError("Error on Getting BrokerSmsTexts Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<BrokerSmsModel> brokerSmsTexts = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {SelectedBrokersCount} BrokerSmsTexts Data Start @ {DateTime} .", brokerSmsTexts.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.BrokerSmsTexts);

            foreach (BrokerSmsModel item in brokerSmsTexts)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.BrokerSmsTexts}{item.SmsId}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync BrokerSmsTexts Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}
