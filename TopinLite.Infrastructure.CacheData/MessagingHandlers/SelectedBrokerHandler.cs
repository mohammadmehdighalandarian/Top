namespace TopinLite.Infrastructure.CacheData.MessagingHandlers
{
    public class SelectedBrokerHandler : IRpcHandler<SelectedBrokerRequestModel, SelectedBrokerResponseModel>
    {
        private readonly ServiceProviders.IInMemoryDataProvider _provider;

        public SelectedBrokerHandler(ServiceProviders.IInMemoryDataProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<SelectedBrokerResponseModel>> HandleAsync(RpcContext context, SelectedBrokerRequestModel request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.BrokerId.ToString()) || string.IsNullOrWhiteSpace(request.OfferId.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<SelectedBrokerResponseModel>.Fail("VALIDATION_ERROR", "BrokerId & OfferId is required."));
            }

            SelectedBrokerModel serviceResult = _provider.GetSelectedBrokerByBrokerIdAndOfferId(request.BrokerId, request.OfferId).GetAwaiter().GetResult();

            SelectedBrokerResponseModel response = new()
            {
                BrokerId = serviceResult.BrokerId,
                OfferId = serviceResult.OfferId,
                GiftPercent = serviceResult.GiftPercent,
                SelectedBrokerId = serviceResult.SelectedBrokerId,
                Status = serviceResult.Status
            };

            return ValueTask.FromResult(RpcResult<SelectedBrokerResponseModel>.Ok(response));
        }
    }
}
