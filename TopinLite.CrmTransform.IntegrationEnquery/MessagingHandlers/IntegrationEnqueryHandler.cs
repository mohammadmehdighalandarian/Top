using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.HuaweiApiModel.CRMResponses;
using TopinLite.Domain.Messaging;
using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint;

namespace TopinLite.CrmTransform.IntegrationEnquery.MessagingHandlers
{
    public class IntegrationEnqueryHandler : IRpcHandler<IntegrationEnqueryRequestModel, IntegrationEnqueryResponseModel>
    {
        private readonly IEndpoint _IEndpoint;

        public IntegrationEnqueryHandler(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<IntegrationEnqueryResponseModel>> HandleAsync(RpcContext context, IntegrationEnqueryRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.HuawiMicroGateway.GeneralHuawiResponse response = _IEndpoint.IntegrationEnquiry(new IntegrationEnquiryTcpRequest
                {
                    Mss = request.Mss,
                    PrimaryIdentity = request.PrimaryIdentity,
                    PrimaryOffers = [.. request.PrimaryOffers.Select(x => new IntegrationEnquiryTcpRequestPrimaryOffers
                    {
                        Offer = x.Offer,
                        Type = x.Type
                    })],
                    PrimaryOfferWhiteLists = [.. request.PrimaryOfferWhiteLists.Select(x => new PrimaryOfferWhiteList
                    {
                        offerId = x.offerId,
                        type = x.type
                    })]
                }).GetAwaiter().GetResult();

                return ValueTask.FromResult(RpcResult<IntegrationEnqueryResponseModel>.Ok(new IntegrationEnqueryResponseModel
                {
                    ResponseDesc = response.ResponseDesc,
                    ResponseType = response.ResponseType
                }));
            }
            catch (Exception ex)
            {
                return ValueTask.FromResult(RpcResult<IntegrationEnqueryResponseModel>.Fail("Exception", ex.Message));
            }
        }
    }
}
