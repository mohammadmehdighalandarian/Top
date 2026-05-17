using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.infra.OracleDataAccess.Queries;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface ITradeTypes
    {
        Task<ExecResult> BeginSync();
    }

    public class TradeTypes : ITradeTypes
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
        private readonly ILogger<TradeTypes> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;

        public TradeTypes(ITopinQueryRepository iTopinQueryRepository, ILogger<TradeTypes> logger, IRedisStringStore redis, IRedisPrefixDeleteService redisPrefixDelete)
        {
            _iTopinQueryRepository = iTopinQueryRepository;
            _logger = logger;
            _redis = redis;
            _redisPrefixDelete = redisPrefixDelete;
        }

        public async Task<ExecResult> BeginSync()
        {
            ExecResult<IEnumerable<TradeTypeModel>> dataFromDb = await _iTopinQueryRepository.GetTradeTypes();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError("Error on Getting TradeTypes Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<TradeTypeModel> tradeTypes = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {TradeTypesCount} TradeTypes Data Start @ {DateTime} .", tradeTypes.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.TradeTypes);

            foreach (TradeTypeModel item in tradeTypes)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.TradeTypes}{item.RechargeType}:{item.OperatorId}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync TradeTypes Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}
