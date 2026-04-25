using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.Services.MiniApiCommands
{
    public class QuerySubscriberCommand : IRpcHandler<QuerySubscriberTcpRequest, GeneralHuawiResponse<QuerySubscriberResponse>>
    {
        private readonly IEndpoint _IEndpoint;

        public QuerySubscriberCommand(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<GeneralHuawiResponse<QuerySubscriberResponse>>> HandleAsync(RpcContext context, QuerySubscriberTcpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse<QuerySubscriberResponse> response = _IEndpoint.QuerySubscriber(new QuerySubscriberTcpRequest
                {
                    Mss = request.Mss,
                    PrimaryIdentity = request.PrimaryIdentity
                }).GetAwaiter().GetResult();

                return ValueTask.FromResult(RpcResult<GeneralHuawiResponse<QuerySubscriberResponse>>.Ok(response));
            }
            catch (Exception ex)
            {
                return ValueTask.FromResult(RpcResult<GeneralHuawiResponse<QuerySubscriberResponse>>.Fail("Exception", ex.Message));
            }
        }
    }
}