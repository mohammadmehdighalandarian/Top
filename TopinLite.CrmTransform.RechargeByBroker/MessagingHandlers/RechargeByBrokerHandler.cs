using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Messaging;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.CrmTransform.RechargeByBroker.MessagingHandlers;

public class RechargeByBrokerHandler : IRpcHandler<RechargeByBrokerRequestModel, RechargeByBrokerResponseModel>
{
    private readonly IEndpoint _IEndpoint;

    public RechargeByBrokerHandler(IEndpoint iEndpoint)
    {
        _IEndpoint = iEndpoint;
    }

    public ValueTask<RpcResult<RechargeByBrokerResponseModel>> HandleAsync(RpcContext context, RechargeByBrokerRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            Domain.HuawiMicroGateway.GeneralHuawiResponse response = _IEndpoint.RechargeByBroker(new RechargeByBrokerTcpRequest
            {
                Amount = request.Amount,
                BrokerId = request.BrokerId,
                Mss = request.Mss,
                PrimaryIdentity = request.PrimaryIdentity,
                RechargeChannelID = request.RechargeChannelID,
                TradeType = request.TradeType,
                BeId = request.BeId
            }).GetAwaiter().GetResult();

            return ValueTask.FromResult(RpcResult<RechargeByBrokerResponseModel>.Ok(new RechargeByBrokerResponseModel
            {
                ResponseDesc =  response.ResponseDesc,
                ResponseType =  response.ResponseType
            }));
        }
        catch (Exception ex)
        {
            return ValueTask.FromResult(RpcResult<RechargeByBrokerResponseModel>.Fail("Exception", ex.Message));
        }
    }
}