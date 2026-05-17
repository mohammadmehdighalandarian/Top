using TopinLite.Infrastructure.CacheData.ServiceProviders;

namespace TopinLite.Infrastructure.CacheData.MessagingHandlers;

public class DiyDataAccessHandler : IRpcHandler<DiyDataAccessRequestModel, int>
{
    private readonly IInMemoryDataProvider _provider;

    public DiyDataAccessHandler(IInMemoryDataProvider provider)
    {
        _provider = provider;
    }
    
    public ValueTask<RpcResult<int>> HandleAsync(RpcContext context, DiyDataAccessRequestModel request, CancellationToken cancellationToken)
    {
        int serviceResult = _provider.GetDiyDataAccess(request.BrokerId, request.OfferId, request.AttributeVal).GetAwaiter().GetResult();

        return ValueTask.FromResult(RpcResult<int>.Ok(serviceResult));
    }
}