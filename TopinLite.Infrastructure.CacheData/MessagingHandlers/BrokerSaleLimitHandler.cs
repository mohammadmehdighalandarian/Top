using TopinLite.Infrastructure.CacheData.ServiceProviders;

namespace TopinLite.Infrastructure.CacheData.MessagingHandlers;

public class BrokerSaleLimitHandler : IRpcHandler<BrokerSaleLimitRequestModel, BrokerSaleLimitResponseModel>
{
    private readonly ServiceProviders.IInMemoryDataProvider _provider;

    public BrokerSaleLimitHandler(IInMemoryDataProvider provider)
    {
        _provider = provider;
    }

    public ValueTask<RpcResult<BrokerSaleLimitResponseModel>> HandleAsync(RpcContext context,
        BrokerSaleLimitRequestModel request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.BrokerId.ToString()))
        {
            return ValueTask.FromResult(
                RpcResult<BrokerSaleLimitResponseModel>.Fail("VALIDATION_ERROR", "BrokerId is required."));
        }

        BrokerSaleLimitModel serviceResult = _provider.GetBrokerSaleLimitByBrokerId(request.BrokerId).GetAwaiter().GetResult();

        BrokerSaleLimitResponseModel response = new BrokerSaleLimitResponseModel
        {
            BrokerId = request.BrokerId,
            RetryLimit = serviceResult.RetryLimit,
            TimeLimit =  serviceResult.TimeLimit
        };

        return ValueTask.FromResult(RpcResult<BrokerSaleLimitResponseModel>.Ok(response));
    }
}