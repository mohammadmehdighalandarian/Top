using System.Text.Json;
using TopinLite.Domain.Commons;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;
using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

namespace TopinLite.Workers.RedisSyncWorker.SyncOperations
{
    public interface IMessages
    {
        Task<ExecResult> BeginSync();
    }

    public class Messages : IMessages
    {
        private readonly infra.OracleDataAccess.Queries.ITopinQueryRepository _iTopinQueryRepository;
        private readonly ILogger<Messages> _logger;
        private readonly IRedisStringStore _redis;
        private readonly IRedisPrefixDeleteService _redisPrefixDelete;


        public Messages(infra.OracleDataAccess.Queries.ITopinQueryRepository iTopinQueryRepository,
            ILogger<Messages> logger,
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
            ExecResult<IEnumerable<MessagesModel>> dataFromDb = await _iTopinQueryRepository.GetMessages();

            if (!dataFromDb.ExecStatus)
            {
                _logger.LogError("Error on Getting Messages Data From Database => {ResultMessage}", dataFromDb.ResultMessage);
                return new ExecResult
                {
                    ExecStatus = false
                };
            }

            List<MessagesModel> messages = dataFromDb.Data.ToList();
            _logger.LogInformation("Sync {dynamicConditionList} Messages Data Start @ {dateTime} .", messages.Count, DateTime.Now);

            var deleteRedisResult = await _redisPrefixDelete.DeleteByPrefixAsync(MemoryDataKeys.Messages);

            foreach (MessagesModel item in messages)
            {
                bool redisResult = await _redis.SetAsync($"{MemoryDataKeys.Messages}{item.MessageId}*{item.MethodName}", JsonSerializer.Serialize(item));
            }

            _logger.LogInformation("Sync Messages Finished @ {DateTime} .", DateTime.Now);

            return new ExecResult
            {
                ExecStatus = true,
                ResultMessage = "Success",
                ResultCode = 0
            };
        }
    }
}
