using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations;

public interface IPrimaryOffers
{
    Task<ExecResult> BeginSync();
}

public class PrimaryOffers : IPrimaryOffers
{
    private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
    private readonly ILogger<PrimaryOffers> _logger;
    private readonly IRedisStringStore _redis;
    private readonly IRedisPrefixDeleteService _redisPrefixDelete;
    
    public PrimaryOffers(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
        ILogger<PrimaryOffers> logger,
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
        ExecResult<IEnumerable<PrimaryOffersModel>> dataFromDb = await _iTopinQueryRepository.GetAllPrimaryOffers();

        if (!dataFromDb.ExecStatus)
        {
            _logger.LogError($"Error on Getting PrimaryOffers Data From Database => {dataFromDb.ResultMessage}");
            return new ExecResult
            {
                ExecStatus = false
            };
        }

        List<PrimaryOffersModel> primaryOfferList = dataFromDb.Data.ToList();
        _logger.LogInformation("Sync {OfferListCount} PrimaryOffers Data Start @ {DateTime} .", primaryOfferList.Count, DateTime.Now);

        var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.PrimaryOffers);

        foreach (PrimaryOffersModel item in primaryOfferList)
        {
            bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.PrimaryOffers}{item.OfferId}", JsonSerializer.Serialize(item));
        }

        _logger.LogInformation("Sync PrimaryOffers Finished @ {DateTime} .", DateTime.Now);

        return new ExecResult
        {
            ExecStatus = true,
            ResultMessage = "Success",
            ResultCode = 0
        };
    }
}