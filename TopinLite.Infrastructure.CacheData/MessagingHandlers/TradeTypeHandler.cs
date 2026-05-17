namespace TopinLite.Infrastructure.CacheData.MessagingHandlers
{
    public class TradeTypeHandler : IRpcHandler<TradeTypeRequestModel, TradeTypeResponseModel>
    {
        private readonly ServiceProviders.IInMemoryDataProvider _provider;

        public TradeTypeHandler(ServiceProviders.IInMemoryDataProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<TradeTypeResponseModel>> HandleAsync(RpcContext context, TradeTypeRequestModel request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.RechargeType.ToString()) || string.IsNullOrWhiteSpace(request.OperatorId.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<TradeTypeResponseModel>.Fail("VALIDATION_ERROR", "RechargeType & OperatorId is required."));
            }

            TradeTypeModel serviceResult = _provider.GetTradeTypeByRechargeTypeAndOperationId(request.RechargeType, request.OperatorId).GetAwaiter().GetResult();

            TradeTypeResponseModel response = new()
            {
                OperatorId = serviceResult.OperatorId,
                OperatorName = serviceResult.OperatorName,
                RechargeDesc = serviceResult.RechargeDesc,
                RechargeType = serviceResult.RechargeType
            };

            return ValueTask.FromResult(RpcResult<TradeTypeResponseModel>.Ok(response));
        }
    }
}
