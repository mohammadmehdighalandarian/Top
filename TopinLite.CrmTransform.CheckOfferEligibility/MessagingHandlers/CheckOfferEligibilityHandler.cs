using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Messaging;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.CrmTransform.CheckOfferEligibility.MessagingHandlers;

public class CheckOfferEligibilityHandler : IRpcHandler<CheckOfferEligibilityRequestModel, CheckOfferEligibilityResponseModel>
{
    private readonly IEndpoint _IEndpoint;

    public CheckOfferEligibilityHandler(IEndpoint iEndpoint)
    {
        _IEndpoint = iEndpoint;
    }

    public ValueTask<RpcResult<CheckOfferEligibilityResponseModel>> HandleAsync(RpcContext context, CheckOfferEligibilityRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            TopinLite.Domain.HuawiMicroGateway.GeneralHuawiResponse response = _IEndpoint.CheckOfferEligibility(new CheckOfferEligibilityTcpRequest
            {
                Mss = request.Mss,
                PrimaryIdentity = request.PrimaryIdentity,
                OfferId = request.OfferId
            }).GetAwaiter().GetResult();

            return ValueTask.FromResult(RpcResult<CheckOfferEligibilityResponseModel>.Ok(new CheckOfferEligibilityResponseModel
            {
                ResponseDesc =  response.ResponseDesc,
                ResponseType =  response.ResponseType
            }));
        }
        catch (Exception ex)
        {
            return ValueTask.FromResult(RpcResult<CheckOfferEligibilityResponseModel>.Fail("Exception", ex.Message));
        }
    }
}