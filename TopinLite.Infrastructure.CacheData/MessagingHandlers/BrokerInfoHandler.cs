namespace TopinLite.Infrastructure.CacheData.MessagingHandlers;

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
                return ValueTask.FromResult(RpcResult<BrokersResponseModel>.Fail("VALIDATION_ERROR", "SapId is required."));
            }

            BrokersModel serviceResult = _provider.GetBrokerInfoBySapId(request.SapId).GetAwaiter().GetResult();

            BrokersResponseModel response = new()
            {
                BrokerId = serviceResult.BrokerId,
                SapId = serviceResult.SapId,
                Status = serviceResult.Status,
                StrPass = serviceResult.StrPass
            };

        return ValueTask.FromResult(RpcResult<BrokersResponseModel>.Ok(response));
    }
}