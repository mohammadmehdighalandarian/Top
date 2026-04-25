using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.Services.MiniApiCommands
{
    public class QuerySimTypeCommand : IRpcHandler<QuerySimTypeTcpRequest, GeneralHuawiResponse>
    {
        private readonly IEndpoint _IEndpoint;

        public QuerySimTypeCommand(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<GeneralHuawiResponse>> HandleAsync(RpcContext context, QuerySimTypeTcpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse response = _IEndpoint.QuerySimType(new QuerySimTypeTcpRequest
                {
                    Mss = request.Mss,
                    SubscriberNo = request.SubscriberNo
                }).GetAwaiter().GetResult();

                return ValueTask.FromResult(RpcResult<GeneralHuawiResponse>.Ok(response));
            }
            catch (Exception ex)
            {
                return ValueTask.FromResult(RpcResult<GeneralHuawiResponse>.Fail("Exception", ex.Message));
            }
        }
    }
}