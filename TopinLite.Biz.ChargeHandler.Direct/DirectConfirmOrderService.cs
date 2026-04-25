using System.Globalization;

using TopinLite.Biz.ChargeHandler.Direct.Validation;
using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeHandler.Direct;

public interface IDirectConfirmOrderService
{
    Task<ExecResult<DirectConfirmOrderResponseModel>> ConfirmAsync(DirectConfirmOrderRequestModel request, CancellationToken cancellationToken);
}

public sealed class DirectConfirmOrderService : IDirectConfirmOrderService
{
    private const decimal InternalErrorCode = 7;
    private const decimal DirectTradeType = 1;

    private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _endpoint;
    private readonly ITopupMessageProvider _messageProvider;

    public DirectConfirmOrderService(
        Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint endpoint,
        ITopupMessageProvider messageProvider)
    {
        _endpoint = endpoint;
        _messageProvider = messageProvider;
    }

    public async Task<ExecResult<DirectConfirmOrderResponseModel>> ConfirmAsync(DirectConfirmOrderRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ExtraCharge != 1001 && request.ExtraCharge != 1002)
            {
                return await BuildErrorAsync(InternalErrorCode, "TOPUP_ERROR", cancellationToken).ConfigureAwait(false);
            }

            GeneralHuawiResponse response = await _endpoint.RechargeByBroker(new RechargeByBrokerTcpRequest
            {
                PrimaryIdentity = request.TelCharger,
                Amount = (long)request.FaceAmount,
                BrokerId = request.SapId.ToString(CultureInfo.InvariantCulture),
                RechargeChannelID = request.ChannelId.ToString(CultureInfo.InvariantCulture),
                TradeType = DirectTradeType.ToString(CultureInfo.InvariantCulture),
                BeId = null,
                Mss = $"Topup/Topin{Guid.NewGuid()}"
            }).ConfigureAwait(false);

            decimal code = ResponseCodeParser.ParseDecimalOrFallback(response.ResponseType, InternalErrorCode);
            string message = await _messageProvider.ResolveAsync(code, response.ResponseDesc, cancellationToken).ConfigureAwait(false);

            return new ExecResult<DirectConfirmOrderResponseModel>
            {
                ExecStatus = code == 0,
                ResultCode = code,
                ResultMessage = message,
                Data = new DirectConfirmOrderResponseModel
                {
                    AccountName = request.CardNo,
                    Remain = response.ResponseDesc,
                    TradeType = DirectTradeType.ToString(CultureInfo.InvariantCulture),
                    RechargeSerialNo = response.ResponseDesc,
                    LoyaltyYear = 0,
                    ResultRaw = response.ResponseDesc
                }
            };
        }
        catch
        {
            return await BuildErrorAsync(InternalErrorCode, "TOPUP_ERROR", cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task<ExecResult<DirectConfirmOrderResponseModel>> BuildErrorAsync(decimal code, string defaultMessage, CancellationToken cancellationToken)
    {
        string message = await _messageProvider.ResolveAsync(code, defaultMessage, cancellationToken).ConfigureAwait(false);
        return new ExecResult<DirectConfirmOrderResponseModel>
        {
            ExecStatus = false,
            ResultCode = code,
            ResultMessage = message,
            Data = new DirectConfirmOrderResponseModel()
        };
    }
}
