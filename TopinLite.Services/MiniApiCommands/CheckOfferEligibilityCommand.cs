using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.Services.MiniApiCommands
{
    public class CheckOfferEligibilityCommand : IRpcHandler<CheckOfferEligibilityTcpRequest, GeneralHuawiResponse>
    {
        private readonly IEndpoint _IEndpoint;

        public CheckOfferEligibilityCommand(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<GeneralHuawiResponse>> HandleAsync(RpcContext context, CheckOfferEligibilityTcpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse response = _IEndpoint.CheckOfferEligibility(new CheckOfferEligibilityTcpRequest
                {
                    Mss = request.Mss,
                    PrimaryIdentity = request.PrimaryIdentity,
                    OfferId = request.OfferId
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