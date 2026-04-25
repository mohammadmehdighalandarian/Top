namespace TopinLite.Infrastructure.CacheData.MessagingHandlers
{

    public sealed class DynamicConditionHandler : IRpcHandler<DynamicConditionsRequestModel, DynamicConditionsResponseModel>
    {
        private readonly ServiceProviders.IInMemoryDataProvider _provider;

        public DynamicConditionHandler(ServiceProviders.IInMemoryDataProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<DynamicConditionsResponseModel>> HandleAsync(
            RpcContext context,
            DynamicConditionsRequestModel request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.DynamicConditionId.ToString()) || string.IsNullOrWhiteSpace(request.Biztype.ToString()) || string.IsNullOrWhiteSpace(request.KeyStr.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<DynamicConditionsResponseModel>.Fail("VALIDATION_ERROR", "DynamicConditionId & Biztype & KeyStr is required."));
            }

            DynamicConditionModel ServiceResult = _provider.GetDynamicConditionsByParameters(request.DynamicConditionId, request.Biztype, request.KeyStr).GetAwaiter().GetResult();

            DynamicConditionsResponseModel response = new DynamicConditionsResponseModel
            {
                KeyStr = ServiceResult.KeyStr,
                Biztype = ServiceResult.Biztype,
                DynamicConditionId = ServiceResult.DynamicConditionId,
                Reserve1 = ServiceResult.Reserve1,
                Reserve2 = ServiceResult.Reserve2,
                Reserve3 = ServiceResult.Reserve3,
                ValueStr = ServiceResult.ValueStr
            };

            return ValueTask.FromResult(RpcResult<DynamicConditionsResponseModel>.Ok(response));
        }
    }
}
