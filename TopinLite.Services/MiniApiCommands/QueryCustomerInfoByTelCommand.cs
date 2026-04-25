using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.Services.MiniApiCommands
{
    public class QueryCustomerInfoByTelCommand : IRpcHandler<QueryCustomerInfoByTelTcpRequest, GeneralHuawiResponse>
    {
        private readonly IEndpoint _IEndpoint;

        public QueryCustomerInfoByTelCommand(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<GeneralHuawiResponse>> HandleAsync(RpcContext context, QueryCustomerInfoByTelTcpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse response = _IEndpoint.QueryCustomerInfoByTel(new QueryCustomerInfoByTelTcpRequest
                {
                    Mss = request.Mss,
                    PrimaryIdentity = request.PrimaryIdentity
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