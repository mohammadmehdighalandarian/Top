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
    public interface IOffers
    {
        Task<ExecResult> BeginSync();
    }

    public class Offers : IOffers
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _ITopinQueryRepository;
        private readonly ILogger<Offers> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public Offers(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
                      ILogger<Offers> logger,
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
            ExecResult<IEnumerable<OffersModel>> DataFromDb = await _ITopinQueryRepository.GetAllOffers();

            if (!DataFromDb.ExecStatus)
            {
                _logger.LogError($"Error on Getting Offers Data From Database => {DataFromDb.ResultMessage}");
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<OffersModel> OfferList = DataFromDb.Data.ToList();
            _logger.LogInformation($"Sync {OfferList.Count} Offers Data Start @ {DateTime.Now} .");

            var DeleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.Offers);

            foreach (var item in OfferList)
            {
                bool RedisResult = await _redis.SetAsync($"{MemoryDataKeys.Offers}{item.OfferId}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation($"Sync Offers Finished @ {DateTime.Now} .");

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}