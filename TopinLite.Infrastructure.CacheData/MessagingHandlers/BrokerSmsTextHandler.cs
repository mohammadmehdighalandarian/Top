using TopinLite.Infrastructure.CacheData.ServiceProviders;

namespace TopinLite.Infrastructure.CacheData.MessagingHandlers
{
    public class BrokerSmsTextHandler : IRpcHandler<BrokerSmsRequestModel, BrokerSmsResponseModel>
    {
        private readonly ServiceProviders.IInMemoryDataProvider _provider;

        public BrokerSmsTextHandler(IInMemoryDataProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<BrokerSmsResponseModel>> HandleAsync(RpcContext context, BrokerSmsRequestModel request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.BrokerId.ToString()) || string.IsNullOrWhiteSpace(request.RequestType.ToString()) || string.IsNullOrWhiteSpace(request.SmsId.ToString()))
            {
                return ValueTask.FromResult(RpcResult<BrokerSmsResponseModel>.Fail("VALIDATION_ERROR", "BrokerId & RequestType & SmsId is required."));
            }

            BrokerSmsModel serviceResult = _provider.GetBrokerSmsTextByBrokerIdAndRequestTypeAndSmsId(request.SmsId, request.BrokerId, request.RequestType).GetAwaiter().GetResult();

            BrokerSmsResponseModel response = new BrokerSmsResponseModel
            {
                BrokerId = request.BrokerId,
                RequestType = serviceResult.RequestType,
                SmsId = serviceResult.SmsId
            };

            return ValueTask.FromResult(RpcResult<BrokerSmsResponseModel>.Ok(response));
        }
    }
}
