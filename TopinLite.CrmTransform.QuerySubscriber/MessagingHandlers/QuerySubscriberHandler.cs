using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.Messaging;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.CrmTransform.QuerySubscriber.MessagingHandlers
{
    public class QuerySubscriberHandler : IRpcHandler<QuerySubscriberRequestModel, QuerySubscriberResponseModel>
    {
        private readonly IEndpoint _IEndpoint;

        public QuerySubscriberHandler(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<QuerySubscriberResponseModel>> HandleAsync(RpcContext context, QuerySubscriberRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.HuawiMicroGateway.GeneralHuawiResponse response = _IEndpoint.QuerySubscriber(new QuerySubscriberTcpRequest
                {
                    Mss = request.Mss,
                    PrimaryIdentity = request.PrimaryIdentity
                }).GetAwaiter().GetResult();

                return ValueTask.FromResult(RpcResult<QuerySubscriberResponseModel>.Ok(new QuerySubscriberResponseModel
                {
                    ResponseDesc = response.ResponseDesc,
                    ResponseType = response.ResponseType
                }));
            }
            catch (Exception ex)
            {
                return ValueTask.FromResult(RpcResult<QuerySubscriberResponseModel>.Fail("Exception", ex.Message));
            }
        }
    }
}
