namespace TopinLite.Infrastructure.CacheData.MessagingHandlers
{
    public sealed class OfferInfoHandler : IRpcHandler<OfferRequestModel, OfferResponseModel>
    {
        private readonly ServiceProviders.IInMemoryDataProvider _provider;

        public OfferInfoHandler(ServiceProviders.IInMemoryDataProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<OfferResponseModel>> HandleAsync(
            RpcContext context,
            OfferRequestModel request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.OfferCode.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<OfferResponseModel>.Fail("VALIDATION_ERROR", "OfferCode is required."));
            }

            OffersModel serviceResult = _provider.GetOfferInfoByOfferId(request.OfferCode).GetAwaiter().GetResult();

            OfferResponseModel response = new OfferResponseModel
            {
                OfferId = serviceResult.OfferId,
                Category = serviceResult.Category,
                CategoryDesc = serviceResult.CategoryDesc,
                Duration = serviceResult.Duration,
                OfferCode = serviceResult.OfferCode,
                OfferName = serviceResult.OfferName,
                PackageVolume = serviceResult.PackageVolume,
                Price = serviceResult.Price,
                RelationId = serviceResult.RelationId,
                RelationName = serviceResult.RelationName,
                Status = serviceResult.Status,
                StatusTitle = serviceResult.StatusTitle,
                SystemId = serviceResult.SystemId,
                SystemType = serviceResult.SystemType,
                Type = serviceResult.Type,
                Type_Desc = serviceResult.Type_Desc,
                RuleId = serviceResult.RuleId,
                BrokerType = serviceResult.BrokerType,
            };

            return ValueTask.FromResult(RpcResult<OfferResponseModel>.Ok(response));
        }
    }
}