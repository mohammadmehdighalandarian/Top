using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Biz.ChargeSaleHandler.ServiceProviders;
using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeSaleHandler.MessagingHandlers;

public sealed class ConfirmOrderChargeHandler : IRpcHandler<ChargeConfirmOrderRequest, ChargeConfirmOrderResponse>
{
    private readonly IChargeServiceProvider _provider;
 
    public ConfirmOrderChargeHandler(IChargeServiceProvider provider)
    {
        _provider = provider;
    }
 
    public async ValueTask<RpcResult<ChargeConfirmOrderResponse>> HandleAsync(RpcContext context,
                                                                              ChargeConfirmOrderRequest request,
                                                                              CancellationToken cancellationToken)
    {
        ExecResult<ChargeConfirmOrderResponse> result = await _provider
            .ConfirmOrderAsync(request, cancellationToken)
            .ConfigureAwait(false);
 
        return result.ExecStatus
            ? RpcResult<ChargeConfirmOrderResponse>.Ok(result.Data!)
            : RpcResult<ChargeConfirmOrderResponse>.Fail(
                result.ResultCode.ToString(System.Globalization.CultureInfo.InvariantCulture),
                result.ResultMessage ?? string.Empty);
    }

}