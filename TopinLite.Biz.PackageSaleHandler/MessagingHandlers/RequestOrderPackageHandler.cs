namespace TopinLite.Biz.PackageSaleHandler.MessagingHandlers;

public sealed class RequestOrderPackageHandler : IRpcHandler<RequestOrderRequestMessageModel, RequestOrderResponseMessageModel>
{
    private readonly IPackageServiceProvider _provider;

    public RequestOrderPackageHandler(IPackageServiceProvider provider)
    {
        _provider = provider;
    }

    public async ValueTask<RpcResult<RequestOrderResponseMessageModel>> HandleAsync(RpcContext context,
                                                                                    RequestOrderRequestMessageModel request,
                                                                                    CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.PayloadId))
        {
            return RpcResult<RequestOrderResponseMessageModel>.Fail("VALIDATION_ERROR", "payloadId is required.");
        }

        ExecResult<RequestOrderResponseMessageModel> result = await _provider
            .RequestOrderAsync(request, cancellationToken)
            .ConfigureAwait(false);

        return result.ExecStatus
            ? RpcResult<RequestOrderResponseMessageModel>.Ok(result.Data!)
            : RpcResult<RequestOrderResponseMessageModel>.Fail(
                result.ResultCode.ToString(CultureInfo.InvariantCulture),
                result.ResultMessage ?? string.Empty);
    }
}
