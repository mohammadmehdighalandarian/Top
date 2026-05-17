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
            if (string.IsNullOrWhiteSpace(request.Biztype) || string.IsNullOrWhiteSpace(request.KeyStr))
            {
                return ValueTask.FromResult(
                    RpcResult<DynamicConditionsResponseModel>.Fail("VALIDATION_ERROR", "Biztype & KeyStr is required."));
            }

            DynamicConditionModel serviceResult = _provider.GetDynamicConditionsByParameters(request.Biztype, request.KeyStr).GetAwaiter().GetResult();

            DynamicConditionsResponseModel response = new()
            {
                KeyStr = serviceResult.KeyStr,
                Biztype = serviceResult.Biztype,
                Reserve1 = serviceResult.Reserve1,
                Reserve2 = serviceResult.Reserve2,
                Reserve3 = serviceResult.Reserve3,
                ValueStr = serviceResult.ValueStr
            };

            return ValueTask.FromResult(RpcResult<DynamicConditionsResponseModel>.Ok(response));
        }
    }
}
