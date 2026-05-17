using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Messaging;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;
using QueryBalanceRequestModel = TopinLite.Domain.Messaging.QueryBalanceRequestModel;

namespace TopinLite.CrmTransform.QueryBalance.MessagingHandlers;

public class QueryBalanceHandler : IRpcHandler<QueryBalanceRequestModel, QueryBalanceResponseModel>
{
    private readonly IEndpoint _IEndpoint;

    public QueryBalanceHandler(IEndpoint iEndpoint)
    {
        _IEndpoint = iEndpoint;
    }

    public ValueTask<RpcResult<QueryBalanceResponseModel>> HandleAsync(RpcContext context, QueryBalanceRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            Domain.HuawiMicroGateway.GeneralHuawiResponse response = _IEndpoint.QueryBalance(new QueryBalanceTcpRequest
            {
                Mss = request.Mss,
                PrimaryIdentity = request.PrimaryIdentity
            }).GetAwaiter().GetResult();

            return ValueTask.FromResult(RpcResult<QueryBalanceResponseModel>.Ok(new QueryBalanceResponseModel
            {
                ResponseDesc =  response.ResponseDesc,
                ResponseType =  response.ResponseType
            }));
        }
        catch (Exception ex)
        {
            return ValueTask.FromResult(RpcResult<QueryBalanceResponseModel>.Fail("Exception", ex.Message));
        }
    }
}