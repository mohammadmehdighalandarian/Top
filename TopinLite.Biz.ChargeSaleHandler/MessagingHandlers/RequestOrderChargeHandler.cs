using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Biz.ChargeSaleHandler.ServiceProviders;
using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeSaleHandler.MessagingHandlers;

public sealed class RequestOrderChargeHandler : IRpcHandler<ChargeRequestOrderRequest, ChargeRequestOrderResponse>
{
    private readonly IChargeServiceProvider _provider;
 
    public RequestOrderChargeHandler(IChargeServiceProvider provider)
    {
        _provider = provider;
    }
 
    public async ValueTask<RpcResult<ChargeRequestOrderResponse>> HandleAsync(RpcContext context, ChargeRequestOrderRequest request, CancellationToken ct)
    {
        ExecResult<ChargeRequestOrderResponse> result = await _provider
            .RequestOrderAsync(request, ct).ConfigureAwait(false);
 
        return result.ExecStatus
            ? RpcResult<ChargeRequestOrderResponse>.Ok(result.Data!)
            : RpcResult<ChargeRequestOrderResponse>.Fail(result.ResultCode.ToString(System.Globalization.CultureInfo.InvariantCulture),
                result.ResultMessage ?? string.Empty);
    }

}