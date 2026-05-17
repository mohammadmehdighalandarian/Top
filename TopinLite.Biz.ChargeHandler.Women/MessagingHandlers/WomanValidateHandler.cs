using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Biz.ChargeHandler.Women.Validation;
using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeHandler.Women.MessagingHandlers
{
    public sealed class WomenValidateHandler
        : IRpcHandler<ChargeTypeValidateRequest, ChargeTypeValidateResponse>
    {
        private readonly IWomenChargeValidationService _validation;
 
        public WomenValidateHandler(IWomenChargeValidationService validation)
        {
            _validation = validation;
        }
 
        public async ValueTask<RpcResult<ChargeTypeValidateResponse>> HandleAsync(
            RpcContext context,
            ChargeTypeValidateRequest request,
            CancellationToken cancellationToken)
        {
            RequestOrderRequestModel legacy = new()
            {
                TelNum = request.TelNum,
                TelGift = request.TelGift,
                Amount = request.Amount,
                PayloadId = request.PayloadId,
                ChannelId = request.ChannelId,
                BrokerId = request.BrokerId,
                CustomerId = request.CustomerId,
                VendorId = request.VendorId,
                AdditionalData = request.AdditionalData!
            };
 
            ExecResult result = await _validation.ValidateAsync(legacy, cancellationToken).ConfigureAwait(false);
 
            return RpcResult<ChargeTypeValidateResponse>.Ok(new ChargeTypeValidateResponse
            {
                ExecStatus = result.ExecStatus,
                ResultCode = result.ResultCode,
                ResultMessage = result.ResultMessage ?? string.Empty
            });
        }
    }
}