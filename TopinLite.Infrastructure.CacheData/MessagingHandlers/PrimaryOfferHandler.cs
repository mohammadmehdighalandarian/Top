namespace TopinLite.Infrastructure.CacheData.MessagingHandlers;

public class PrimaryOfferHandler : IRpcHandler<PrimaryOfferRequestModel, List<PrimaryOfferResponseModel>>
{
    private readonly ServiceProviders.IInMemoryDataProvider _provider;

    public PrimaryOfferHandler(ServiceProviders.IInMemoryDataProvider provider)
    {
        _provider = provider;
    }
    
    public ValueTask<RpcResult<List<PrimaryOfferResponseModel>>> HandleAsync(RpcContext context, PrimaryOfferRequestModel request, CancellationToken cancellationToken)
    {
        List<PrimaryOffersModel> serviceResult = _provider.GetPrimaryOffers().GetAwaiter().GetResult();

        List<PrimaryOfferResponseModel> response = serviceResult.Select(x => new PrimaryOfferResponseModel
        {
            OfferId = x.OfferId,
            Type = x.Type,
            Status = x.Status
        }).ToList();

        return ValueTask.FromResult(RpcResult<List<PrimaryOfferResponseModel>>.Ok(response));
    }
}