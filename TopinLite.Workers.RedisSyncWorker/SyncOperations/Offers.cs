using System.Text.Json;

using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface IOffers
    {
        Task<ExecResult> BeginSync();
    }

    public class Offers : IOffers
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
        private readonly ILogger<Offers> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;

        public Offers(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
                      ILogger<Offers> logger,
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
            ExecResult<IEnumerable<OffersModel>> dataFromDb = await _iTopinQueryRepository.GetAllOffers();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError($"Error on Getting Offers Data From Database => {dataFromDb.ResultMessage}");
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<OffersModel> offerList = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {OfferListCount} Offers Data Start @ {DateTime} .", offerList.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.Offers);

            foreach (OffersModel item in offerList)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.Offers}{item.OfferId}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync Offers Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}