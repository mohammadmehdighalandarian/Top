using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;

using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.PackageSaleHandler.MessagingHandlers
{



    public sealed class RequestOrderPackageHandler : IRpcHandler<RequestOrderRequestMessageModel, RequestOrderResponseMessageModel>
    {
        private readonly ServiceProviders.IPackageServiceProvider _provider;

        public RequestOrderPackageHandler(ServiceProviders.IPackageServiceProvider provider)
        {
            _provider = provider;
        }

        public ValueTask<RpcResult<RequestOrderResponseMessageModel>> HandleAsync(
            RpcContext context,
            RequestOrderRequestMessageModel request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.payloadId))
            {
                return ValueTask.FromResult(
                    RpcResult<RequestOrderResponseMessageModel>.Fail("VALIDATION_ERROR", "payloadId is required."));
            }

            ///OffersModel ServiceResult = _provider.GetOfferInfoByOfferId(request.OfferId).GetAwaiter().GetResult();
            ///
            ExecResult ServiceResult = _provider.InitPackage(request).GetAwaiter().GetResult();

            RequestOrderResponseMessageModel response = new RequestOrderResponseMessageModel
            {
                OrderId = ServiceResult.ResultCode
            };

            return ValueTask.FromResult(RpcResult<RequestOrderResponseMessageModel>.Ok(response));
        }
    }
}
