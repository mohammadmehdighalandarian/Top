using System.Globalization;

using TopinLite.Biz.ChargeHandler.Women.Validation;
using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeHandler.Women;

public interface IWomenConfirmOrderService
{
    Task<ExecResult<WomenConfirmOrderResponseModel>> ConfirmAsync(WomenConfirmOrderRequestModel request, CancellationToken cancellationToken);
}

public sealed class WomenConfirmOrderService : IWomenConfirmOrderService
{
    private const decimal InternalErrorCode = 7;
    private const decimal WomenTradeType = 5;

    private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _endpoint;
    private readonly ITopupMessageProvider _messageProvider;

    public WomenConfirmOrderService(
        Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint endpoint,
        ITopupMessageProvider messageProvider)
    {
        _endpoint = endpoint;
        _messageProvider = messageProvider;
    }

    public async Task<ExecResult<WomenConfirmOrderResponseModel>> ConfirmAsync(WomenConfirmOrderRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ExtraCharge != 1005)
            {
                return await BuildErrorAsync(InternalErrorCode, "TOPUP_ERROR", cancellationToken).ConfigureAwait(false);
            }

            GeneralHuawiResponse response = await _endpoint.RechargeByBroker(new RechargeByBrokerTcpRequest
            {
                PrimaryIdentity = request.TelCharger,
                Amount = (long)request.FaceAmount,
                BrokerId = request.SapId.ToString(CultureInfo.InvariantCulture),
                RechargeChannelID = request.ChannelId.ToString(CultureInfo.InvariantCulture),
                TradeType = WomenTradeType.ToString(CultureInfo.InvariantCulture),
                BeId = null,
                Mss = $"Topup/Topin{Guid.NewGuid()}"
            }).ConfigureAwait(false);

            decimal code = ResponseCodeParser.ParseDecimalOrFallback(response.ResponseType, InternalErrorCode);
            string message = await _messageProvider.ResolveAsync(code, response.ResponseDesc, cancellationToken).ConfigureAwait(false);

            return new ExecResult<WomenConfirmOrderResponseModel>
            {
                ExecStatus = code == 0,
                ResultCode = code,
                ResultMessage = message,
                Data = new WomenConfirmOrderResponseModel
                {
                    AccountName = request.CardNo,
                    Remain = response.ResponseDesc,
                    TradeType = WomenTradeType.ToString(CultureInfo.InvariantCulture),
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

    private async Task<ExecResult<WomenConfirmOrderResponseModel>> BuildErrorAsync(decimal code, string defaultMessage, CancellationToken cancellationToken)
    {
        string message = await _messageProvider.ResolveAsync(code, defaultMessage, cancellationToken).ConfigureAwait(false);
        return new ExecResult<WomenConfirmOrderResponseModel>
        {
            ExecStatus = false,
            ResultCode = code,
            ResultMessage = message,
            Data = new WomenConfirmOrderResponseModel()
        };
    }
}
