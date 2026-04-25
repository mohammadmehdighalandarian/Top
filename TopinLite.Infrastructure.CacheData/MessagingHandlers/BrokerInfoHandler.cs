namespace TopinLite.Infrastructure.CacheData.MessagingHandlers
{
    public sealed class BrokerInfoHandler : IRpcHandler<BrokersRequestModel, BrokersResponseModel>
    {
        private readonly ServiceProviders.IInMemoryDataProvider _provider;

        public BrokerInfoHandler(ServiceProviders.IInMemoryDataProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<BrokersResponseModel>> HandleAsync(
            RpcContext context,
            BrokersRequestModel request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.SapId.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<BrokersResponseModel>.Fail("VALIDATION_ERROR", "SapId is required."));
            }

            BrokersModel ServiceResult = _provider.GetBrokerInfoBySapId(request.SapId).GetAwaiter().GetResult();

            BrokersResponseModel response = new BrokersResponseModel
            {
                BrokerId = ServiceResult.BrokerId,
                BrokerType = ServiceResult.BrokerType,
                FkBroker = ServiceResult.FkBroker,
                SaleAccess = ServiceResult.SaleAccess,
                SapId = ServiceResult.SapId,
                Status = ServiceResult.Status,
                StrPass = ServiceResult.StrPass
            };

            return ValueTask.FromResult(RpcResult<BrokersResponseModel>.Ok(response));
        }
    }
}