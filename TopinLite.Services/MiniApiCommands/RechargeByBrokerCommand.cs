using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.Services.MiniApiCommands
{
    public class RechargeByBrokerCommand : IRpcHandler<RechargeByBrokerTcpRequest, GeneralHuawiResponse>
    {
        private readonly IEndpoint _IEndpoint;

        public RechargeByBrokerCommand(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<GeneralHuawiResponse>> HandleAsync(RpcContext context, RechargeByBrokerTcpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse response = _IEndpoint.RechargeByBroker(new RechargeByBrokerTcpRequest
                {
                    Amount = request.Amount,
                    BrokerId = request.BrokerId,
                    Mss = request.Mss,
                    PrimaryIdentity = request.PrimaryIdentity,
                    RechargeChannelID = request.RechargeChannelID,
                    TradeType = request.TradeType,
                    BeId = request.BeId
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