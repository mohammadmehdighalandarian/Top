using TopinLite.Infrastructure.CacheData.ServiceProviders;

namespace TopinLite.Infrastructure.CacheData.MessagingHandlers
{
    public class MessagesHandler : IRpcHandler<MessagesRequestModel, MessagesResponseModel>
    {
        private readonly IInMemoryDataProvider _provider;

        public MessagesHandler(IInMemoryDataProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<MessagesResponseModel>> HandleAsync(RpcContext context, MessagesRequestModel request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.MessageId.ToString()) || string.IsNullOrWhiteSpace(request.MethodName))
            {
                return ValueTask.FromResult(RpcResult<MessagesResponseModel>.Fail("VALIDATION_ERROR", "MessageId & MethodName is required."));
            }

            MessagesModel serviceResult = _provider.GetMessageByIdAndMethod(request.MessageId, request.MethodName).GetAwaiter().GetResult();

            MessagesResponseModel response = new MessagesResponseModel
            {
                MessageId = request.MessageId,
                MethodName = serviceResult.MethodName,
                MessageText = serviceResult.MessageText
            };

            return ValueTask.FromResult(RpcResult<MessagesResponseModel>.Ok(response));
        }
    }
}
