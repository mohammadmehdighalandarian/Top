using System.Globalization;

using TopinLite.Biz.ChargeHandler.Loyalty.Validation;
using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeHandler.Loyalty;

public interface ILoyaltyConfirmOrderService
{
    Task<ExecResult<LoyaltyConfirmOrderResponseModel>> ConfirmAsync(LoyaltyConfirmOrderRequestModel request, CancellationToken cancellationToken);
}

public sealed class LoyaltyConfirmOrderService : ILoyaltyConfirmOrderService
{
    private const decimal InternalErrorCode = 7;
    private const decimal InvalidLoyaltyCode = -1028;
    private const decimal LoyaltyTradeType = 6;

    private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _endpoint;
    private readonly ITopupMessageProvider _messageProvider;

    public LoyaltyConfirmOrderService(
        Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint endpoint,
        ITopupMessageProvider messageProvider)
    {
        _endpoint = endpoint;
        _messageProvider = messageProvider;
    }

    public async Task<ExecResult<LoyaltyConfirmOrderResponseModel>> ConfirmAsync(LoyaltyConfirmOrderRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ExtraCharge != 1006)
            {
                return await BuildErrorAsync(InternalErrorCode, "TOPUP_ERROR", cancellationToken).ConfigureAwait(false);
            }

            (bool isEligible, decimal loyaltyYear) = await ResolveLoyaltyAsync(request.TelCharger).ConfigureAwait(false);
            if (!isEligible)
            {
                string message = await _messageProvider.ResolveAsync(
                    InvalidLoyaltyCode,
                    "Loyalty charge is not allowed for this subscriber.",
                    cancellationToken).ConfigureAwait(false);

                return new ExecResult<LoyaltyConfirmOrderResponseModel>
                {
                    ExecStatus = false,
                    ResultCode = InvalidLoyaltyCode,
                    ResultMessage = message,
                    Data = new LoyaltyConfirmOrderResponseModel
                    {
                        LoyaltyYear = (int)loyaltyYear,
                        TradeType = LoyaltyTradeType.ToString(CultureInfo.InvariantCulture)
                    }
                };
            }

            GeneralHuawiResponse response = await _endpoint.RechargeByBroker(new RechargeByBrokerTcpRequest
            {
                PrimaryIdentity = request.TelCharger,
                Amount = (long)request.FaceAmount,
                BrokerId = request.SapId.ToString(CultureInfo.InvariantCulture),
                RechargeChannelID = request.ChannelId.ToString(CultureInfo.InvariantCulture),
                TradeType = LoyaltyTradeType.ToString(CultureInfo.InvariantCulture),
                BeId = null,
                Mss = $"Topup/Topin{Guid.NewGuid()}"
            }).ConfigureAwait(false);

            decimal code = ResponseCodeParser.ParseDecimalOrFallback(response.ResponseType, InternalErrorCode);
            string resolvedMessage = await _messageProvider.ResolveAsync(code, response.ResponseDesc, cancellationToken).ConfigureAwait(false);

            return new ExecResult<LoyaltyConfirmOrderResponseModel>
            {
                ExecStatus = code == 0,
                ResultCode = code,
                ResultMessage = resolvedMessage,
                Data = new LoyaltyConfirmOrderResponseModel
                {
                    AccountName = request.CardNo,
                    Remain = response.ResponseDesc,
                    TradeType = LoyaltyTradeType.ToString(CultureInfo.InvariantCulture),
                    RechargeSerialNo = response.ResponseDesc,
                    LoyaltyYear = (int)loyaltyYear,
                    ResultRaw = response.ResponseDesc
                }
            };
        }
        catch
        {
            return await BuildErrorAsync(InternalErrorCode, "TOPUP_ERROR", cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task<(bool isEligible, decimal loyaltyYear)> ResolveLoyaltyAsync(string telCharger)
    {
        try
        {
            GeneralHuawiResponse<Domain.HuaweiApiModel.CRMResponses.QuerySubscriber.QuerySubscriberResponse> response =
                await _endpoint.QuerySubscriber(new QuerySubscriberTcpRequest
                {
                    PrimaryIdentity = telCharger,
                    Mss = $"Topup/Topin{Guid.NewGuid()}"
                }).ConfigureAwait(false);

            if (!string.Equals(response.ResponseType, "0", StringComparison.Ordinal))
            {
                return (false, 0);
            }

            string activeDateToken = TokenParser.GetToken(response.ResponseDesc, 8, ';');
            if (!DateParser.TryParse(activeDateToken, out DateTime activeDate, DateParser.SubscriberDateFormats))
            {
                return (false, 0);
            }

            int activeMonths = DiffMonths(DateTime.UtcNow, activeDate);
            if (activeMonths < 12)
            {
                return (false, 0);
            }

            return (true, Math.Max(1, activeMonths / 12m));
        }
        catch
        {
            return (false, 0);
        }
    }

    private async Task<ExecResult<LoyaltyConfirmOrderResponseModel>> BuildErrorAsync(decimal code, string defaultMessage, CancellationToken cancellationToken)
    {
        string message = await _messageProvider.ResolveAsync(code, defaultMessage, cancellationToken).ConfigureAwait(false);
        return new ExecResult<LoyaltyConfirmOrderResponseModel>
        {
            ExecStatus = false,
            ResultCode = code,
            ResultMessage = message,
            Data = new LoyaltyConfirmOrderResponseModel()
        };
    }

    private static int DiffMonths(DateTime newer, DateTime older)
    {
        if (newer < older)
        {
            return 0;
        }

        int months = (newer.Year - older.Year) * 12 + newer.Month - older.Month;
        if (newer.Day < older.Day)
        {
            months--;
        }

        return Math.Max(0, months);
    }
}
