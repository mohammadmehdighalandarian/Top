using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.Messaging;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.CrmTransform.QueryCustomerInfoByTel.MessagingHandlers;

public class QueryCustomerByTelHandler : IRpcHandler<QueryCustomerByTelRequestModel, QueryCustomerByTelResponseModel>
{
    private readonly IEndpoint _IEndpoint;

    public QueryCustomerByTelHandler(IEndpoint iEndpoint)
    {
        _IEndpoint = iEndpoint;
    }

    public ValueTask<RpcResult<QueryCustomerByTelResponseModel>> HandleAsync(RpcContext context, QueryCustomerByTelRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            Domain.HuawiMicroGateway.GeneralHuawiResponse response = _IEndpoint.QueryCustomerInfoByTel(new QueryCustomerInfoByTelTcpRequest
            {
                Mss = request.Mss,
                PrimaryIdentity = request.PrimaryIdentity
            }).GetAwaiter().GetResult();

            return ValueTask.FromResult(RpcResult<QueryCustomerByTelResponseModel>.Ok(new QueryCustomerByTelResponseModel
            {
                ResponseDesc =  response.ResponseDesc,
                ResponseType =  response.ResponseType
            }));
        }
        catch (Exception ex)
        {
            return ValueTask.FromResult(RpcResult<QueryCustomerByTelResponseModel>.Fail("Exception", ex.Message));
        }
    }
}