using TopinLite.Infrastructure.CacheData.ServiceProviders;

namespace TopinLite.Infrastructure.CacheData.MessagingHandlers;

public class DiyPriceHandler : IRpcHandler<DiyPriceRequestModel, decimal>
{
    private readonly IInMemoryDataProvider _provider;

    public DiyPriceHandler(IInMemoryDataProvider provider)
    {
        _provider = provider;
    }
    
    public ValueTask<RpcResult<decimal>> HandleAsync(RpcContext context, DiyPriceRequestModel request, CancellationToken cancellationToken)
    {
        decimal serviceResult = _provider.GetDiyPriceByUnits(request.Data, request.Voice, request.Sms).GetAwaiter().GetResult();

        return ValueTask.FromResult(RpcResult<decimal>.Ok(serviceResult));
    }
}