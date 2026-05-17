namespace TopinLite.Biz.PackageSaleHandler.MessagingHandlers;

public sealed class ConfirmOrderPackageHandler : IRpcHandler<PackageConfirmOrderRequest, PackageConfirmOrderResponse>
{
    private readonly IPackageServiceProvider _provider;

    public ConfirmOrderPackageHandler(IPackageServiceProvider provider)
    {
        _provider = provider;
    }

    public async ValueTask<RpcResult<PackageConfirmOrderResponse>> HandleAsync(RpcContext context,
                                                                               PackageConfirmOrderRequest request,
                                                                               CancellationToken cancellationToken)
    {
        ExecResult<PackageConfirmOrderResponse> result = await _provider
            .ConfirmOrderAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return result.ExecStatus
            ? RpcResult<PackageConfirmOrderResponse>.Ok(result.Data!)
            : RpcResult<PackageConfirmOrderResponse>.Fail(
                result.ResultCode.ToString(CultureInfo.InvariantCulture),
                result.ResultMessage ?? string.Empty);
    }
}
