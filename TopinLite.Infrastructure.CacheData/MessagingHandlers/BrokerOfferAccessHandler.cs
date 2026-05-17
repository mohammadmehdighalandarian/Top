using TopinLite.Infrastructure.CacheData.ServiceProviders;

namespace TopinLite.Infrastructure.CacheData.MessagingHandlers;

public class BrokerOfferAccessHandler : IRpcHandler<BrokerOfferAccessRequestModel, BrokerOfferAccessResponseModel>
{
    private readonly IInMemoryDataProvider _provider;

    public BrokerOfferAccessHandler(IInMemoryDataProvider provider)
    {
        _provider = provider;
    }

    public ValueTask<RpcResult<BrokerOfferAccessResponseModel>> HandleAsync(RpcContext context, BrokerOfferAccessRequestModel request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.BrokerId.ToString()) || string.IsNullOrWhiteSpace(request.OfferId.ToString()))
        {
            return ValueTask.FromResult(
                RpcResult<BrokerOfferAccessResponseModel>.Fail("VALIDATION_ERROR", "BrokerId & OfferId is required."));
        }

        BrokerOfferAccessModel serviceResult = _provider.GetBrokerAccessByOfferAndBrokerId(request.BrokerId, request.OfferId).GetAwaiter().GetResult();

        BrokerOfferAccessResponseModel response = new()
        {
            BrokerId = serviceResult.BrokerId,
            OfferId = serviceResult.OfferId,
            Status = serviceResult.Status
        };

        return ValueTask.FromResult(RpcResult<BrokerOfferAccessResponseModel>.Ok(response));
    }
}