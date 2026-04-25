using StackExchange.Redis.Extensions.Core.Abstractions;

using TopinLite.Domain.TopinApi;


namespace TopinLite.Workers.RedisSyncWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly SyncOperations.IOffers _IOffers;
        private readonly SyncOperations.IBrokers _IBrokers;
        private readonly SyncOperations.IBrokersAccessList _IBrokersAccessList;
        private readonly SyncOperations.IDynamicCondition _IDynamicCondition;

        public Worker(ILogger<Worker> logger,
                      SyncOperations.IOffers iOffers,
                      SyncOperations.IBrokers iBrokers,
                      SyncOperations.IBrokersAccessList iBrokersAccessList,
                      SyncOperations.IDynamicCondition iDynamicCondition)
        {
            _logger = logger;
            _IOffers = iOffers;
            _IBrokers = iBrokers;
            _IBrokersAccessList = iBrokersAccessList;
            _IDynamicCondition = iDynamicCondition;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {



            while (!stoppingToken.IsCancellationRequested)
            {
                ExecResult OfferSyncResult = await _IOffers.BeginSync();
                ExecResult BrokerSyncResult = await _IBrokers.BeginSync();
                ExecResult BrokersAccessSyncResult = await _IBrokersAccessList.BeginSync();
                ExecResult DynamicConditionResult = await _IDynamicCondition.BeginSync();


                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
