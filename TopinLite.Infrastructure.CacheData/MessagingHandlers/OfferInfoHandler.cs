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
            if (string.IsNullOrWhiteSpace(request.OfferId.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<OfferResponseModel>.Fail("VALIDATION_ERROR", "OfferId is required."));
            }

            OffersModel ServiceResult = _provider.GetOfferInfoByOfferId(request.OfferId).GetAwaiter().GetResult();

            OfferResponseModel response = new OfferResponseModel
            {
                OfferId = ServiceResult.OfferId,
                Category = ServiceResult.Category,
                CategoryDesc = ServiceResult.CategoryDesc,
                Duration = ServiceResult.Duration,
                OfferCode = ServiceResult.OfferCode,
                OfferName = ServiceResult.OfferName,
                PackageVolume = ServiceResult.PackageVolume,
                Price = ServiceResult.Price,
                RelationId = ServiceResult.RelationId,
                RelationName = ServiceResult.RelationName,
                Status = ServiceResult.Status,
                StatusTitle = ServiceResult.StatusTitle,
                SystemId = ServiceResult.SystemId,
                SystemType = ServiceResult.SystemType,
                Type = ServiceResult.Type,
                Type_Desc = ServiceResult.Type_Desc
            };

            return ValueTask.FromResult(RpcResult<OfferResponseModel>.Ok(response));
        }
    }
}