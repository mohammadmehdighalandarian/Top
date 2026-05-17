using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Biz.ChargeHandler.Loyalty.Validation;
using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeHandler.Loyalty.MessagingHandlers
{
    public sealed class LoyaltyValidateHandler
        : IRpcHandler<ChargeTypeValidateRequest, ChargeTypeValidateResponse>
    {
        private readonly ILoyaltyChargeValidationService _validation;

        public LoyaltyValidateHandler(ILoyaltyChargeValidationService validation)
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