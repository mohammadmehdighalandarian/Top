using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.Services.MiniApiCommands
{
    public class IntegrationEnquiryCommand : IRpcHandler<IntegrationEnquiryTcpRequest, GeneralHuawiResponse>
    {
        private readonly IEndpoint _IEndpoint;

        public IntegrationEnquiryCommand(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<GeneralHuawiResponse>> HandleAsync(RpcContext context, IntegrationEnquiryTcpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse response = _IEndpoint.IntegrationEnquiry(new IntegrationEnquiryTcpRequest
                {
                    Mss = request.Mss,
                    PrimaryIdentity = request.PrimaryIdentity,
                    PrimaryOffers = request.PrimaryOffers,
                    PrimaryOfferWhiteLists = request.PrimaryOfferWhiteLists
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