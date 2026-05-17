using TopinLite.Domain.TopinApi;
using TopinLite.Workers.RedisSyncWorker.SyncOperations;

namespace TopinLite.Workers.RedisSyncWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOffers _IOffers;
        private readonly IBrokers _IBrokers;
        private readonly IBrokersAccessList _IBrokersAccessList;
        private readonly IDynamicCondition _IDynamicCondition;
        private readonly IDiyPrice _IDiyPrice;
        private readonly IBrokerOfferAccess _IBrokerOfferAccess;
        private readonly IDiyDataAccess _IDiyDataAccess;
        private readonly IPrimaryOffers _iPrimaryOffers;
        private readonly ITradeTypes _iTradeTypes;
        private readonly ISelectedBrokers _selectedBrokers;
        private readonly IBrokerSaleLimit _brokerSaleLimit;
        private readonly IBrokerSmsTexts _brokerSmsTexts;
        private readonly IMessages _messages;

        public Worker(ILogger<Worker> logger,
                      IOffers iOffers,
                      IBrokers iBrokers,
                      IBrokersAccessList iBrokersAccessList,
                      IDynamicCondition iDynamicCondition,
                      IDiyPrice iDiyPrice,
                      IBrokerOfferAccess iBrokerOfferAccess,
                      IDiyDataAccess iDiyDataAccess,
                      IPrimaryOffers iPrimaryOffers, 
                      ITradeTypes iTradeTypes, 
                      ISelectedBrokers selectedBrokers, 
                      IBrokerSaleLimit brokerSaleLimit, 
                      IBrokerSmsTexts brokerSmsTexts, 
                      IMessages messages)
        {
            _logger = logger;
            _IOffers = iOffers;
            _IBrokers = iBrokers;
            _IBrokersAccessList = iBrokersAccessList;
            _IDynamicCondition = iDynamicCondition;
            _IDiyPrice = iDiyPrice;
            _IBrokerOfferAccess = iBrokerOfferAccess;
            _IDiyDataAccess = iDiyDataAccess;
            _iPrimaryOffers = iPrimaryOffers;
            _iTradeTypes = iTradeTypes;
            _selectedBrokers = selectedBrokers;
            _brokerSaleLimit = brokerSaleLimit;
            _brokerSmsTexts = brokerSmsTexts;
            _messages = messages;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ExecResult OfferSyncResult = await _IOffers.BeginSync();
                ExecResult BrokerSyncResult = await _IBrokers.BeginSync();
                ExecResult BrokersAccessSyncResult = await _IBrokersAccessList.BeginSync();
                ExecResult DynamicConditionResult = await _IDynamicCondition.BeginSync();
                ExecResult DiyPriceResult = await _IDiyPrice.BeginSync();
                ExecResult BrokerOfferAccessSyncResult = await _IBrokerOfferAccess.BeginSync();
                ExecResult DiyDataAccessResult = await _IDiyDataAccess.BeginSync();
                ExecResult PrimaryOfferResult = await _iPrimaryOffers.BeginSync();
                ExecResult TradeTypeResult = await _iTradeTypes.BeginSync();
                ExecResult SelectedBrokerResult = await _selectedBrokers.BeginSync();
                ExecResult BrokerSaleLimitResult = await _brokerSaleLimit.BeginSync();
                ExecResult BrokerSmsTextsResult = await _brokerSmsTexts.BeginSync();
                ExecResult BrokerSmsTextsBySmsIdResult = await _brokerSmsTexts.BeginSyncBySmsId();
                ExecResult MessagesResult = await _messages.BeginSync();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}