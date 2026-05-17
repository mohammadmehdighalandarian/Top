using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Messaging;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.CrmTransform.QuerySimType.MessagingHandlers;

public class QuerySimTypeHandler : IRpcHandler<QuerySimTypeRequestModel, QuerySimTypeResponseModel>
{
    private readonly IEndpoint _IEndpoint;

    public QuerySimTypeHandler(IEndpoint iEndpoint)
    {
        _IEndpoint = iEndpoint;
    }

    public ValueTask<RpcResult<QuerySimTypeResponseModel>> HandleAsync(RpcContext context, QuerySimTypeRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            Domain.HuawiMicroGateway.GeneralHuawiResponse response = _IEndpoint.QuerySimType(new QuerySimTypeTcpRequest
            {
                Mss = request.Mss,
                SubscriberNo = request.SubscriberNo
            }).GetAwaiter().GetResult();

            return ValueTask.FromResult(RpcResult<QuerySimTypeResponseModel>.Ok(new QuerySimTypeResponseModel
            {
                ResponseDesc =  response.ResponseDesc,
                ResponseType =  response.ResponseType
            }));
        }
        catch (Exception ex)
        {
            return ValueTask.FromResult(RpcResult<QuerySimTypeResponseModel>.Fail("Exception", ex.Message));
        }
    }
}