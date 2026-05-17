namespace TopinLite.Infrastructure.CacheData.MessagingHandlers
{
    public sealed class BrokersAccessHandler : IRpcHandler<BrokersAccessRequestModel, BrokersAccessResponseModel>
    {
        private readonly ServiceProviders.IInMemoryDataProvider _provider;

        public BrokersAccessHandler(ServiceProviders.IInMemoryDataProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<BrokersAccessResponseModel>> HandleAsync(
            RpcContext context,
            BrokersAccessRequestModel request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.SapId.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<BrokersAccessResponseModel>.Fail("VALIDATION_ERROR", "SapId is required."));
            }

            if (string.IsNullOrWhiteSpace(request.MethodName.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<BrokersAccessResponseModel>.Fail("VALIDATION_ERROR", "MethodName is required."));
            }

            BrokersAccessModel ServiceResult = _provider.GetBrokersAccessBySapIdAndMethodName(request.SapId, request.MethodName).GetAwaiter().GetResult();

            BrokersAccessResponseModel response = new BrokersAccessResponseModel
            {
                MethodName = request.MethodName,
                SapId = request.SapId,
            };

            return ValueTask.FromResult(RpcResult<BrokersAccessResponseModel>.Ok(response));
        }
    }
}