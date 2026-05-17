using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Messaging;

namespace TopinLite.Biz.ChargeSaleHandler.Crm;

public interface ICrmRechargeCaller
{
    Task<RechargeByBrokerResponseModel?> RechargeAsync(
        RechargeByBrokerRequestModel request,
        CancellationToken cancellationToken);
}

public sealed class CrmRechargeCaller : ICrmRechargeCaller
{
    private const string Subject = "crm.rechargebybroker";

    private readonly IRpcClient _rpc;

    public CrmRechargeCaller(IRpcClient rpc)
    {
        _rpc = rpc;
    }

    public async Task<RechargeByBrokerResponseModel?> RechargeAsync(
        RechargeByBrokerRequestModel request,
        CancellationToken cancellationToken)
    {
        RpcResult<RechargeByBrokerResponseModel> result = await _rpc.RequestAsync<
            RechargeByBrokerRequestModel,
            RechargeByBrokerResponseModel>(
            subject: Subject,
            request: request,
            options: new RpcCallOptions { Timeout = TimeSpan.FromSeconds(10) },
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return result.Success ? result.Data : null;
    }
}