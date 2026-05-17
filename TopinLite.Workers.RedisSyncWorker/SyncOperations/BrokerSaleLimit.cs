using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;
using TopinLite.infra.OracleDataAccess.Queries;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations;

public interface IBrokerSaleLimit
{
    Task<ExecResult> BeginSync();
}

public class BrokerSaleLimit : IBrokerSaleLimit
{
    private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
    private readonly ILogger<BrokerSaleLimit> _logger;
    private readonly IRedisStringStore _redis;
    private readonly IRedisPrefixDeleteService _redisPrefixDelete;

    public BrokerSaleLimit(ITopinQueryRepository iTopinQueryRepository, ILogger<BrokerSaleLimit> logger,
        IRedisStringStore redis, IRedisPrefixDeleteService redisPrefixDelete)
    {
        _iTopinQueryRepository = iTopinQueryRepository;
        _logger = logger;
        _redis = redis;
        _redisPrefixDelete = redisPrefixDelete;
    }

    public async Task<ExecResult> BeginSync()
    {
        ExecResult<IEnumerable<BrokerSaleLimitModel>> dataFromDb = await _iTopinQueryRepository.GetBrokerSaleLimits();
        if (!dataFromDb.ExecStatus)
        {
            _logger.LogError("Error on Getting BrokerSaleLimit Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
            return new ExecResult { ExecStatus = false };
        }

        List<BrokerSaleLimitModel> list = dataFromDb.Data.ToList();
        _logger.LogInformation("Sync {ListCount} BrokerSaleLimit Data Start @ {DateTime} .", list.Count, DateTime.Now);

        await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.BrokerSaleLimits);

        foreach (BrokerSaleLimitModel item in list)
        {
            var key = $"{MemoryDataKeys.BrokerSaleLimits}{item.BrokerId}";
            bool redisResult = await _redis.SetAsync(key, JsonSerializer.Serialize(item));
        }

        _logger.LogInformation("Sync DiyDataAccess Finished @ {DateTime} .", DateTime.Now);
        return new ExecResult { ExecStatus = true, ResultMessage = "Success", ResultCode = 0 };
    }
}