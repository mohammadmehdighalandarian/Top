using System.Globalization;
using TopinLite.Biz.ChargeHandler.Youth.Validation;
using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeHandler.Youth;

public interface IYouthConfirmOrderService
{
    Task<ExecResult<YouthConfirmOrderResponseModel>> ConfirmAsync(YouthConfirmOrderRequestModel request, CancellationToken cancellationToken);
}

public sealed class YouthConfirmOrderService : IYouthConfirmOrderService
{
    private const decimal InternalErrorCode = 7;
    private const decimal DynamicGiftOfferErrorCode = -1070;

    private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _endpoint;
    private readonly ITopupMessageProvider _messageProvider;

    public YouthConfirmOrderService(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint endpoint,ITopupMessageProvider messageProvider)
    {
        _endpoint = endpoint;
        _messageProvider = messageProvider;
    }

    public async Task<ExecResult<YouthConfirmOrderResponseModel>> ConfirmAsync(YouthConfirmOrderRequestModel request, CancellationToken cancellationToken)
    {
        try
        {
            decimal tradeType;
            decimal loyaltyYear = 0;

            if (request.ExtraCharge == 1004)
            {
                tradeType = 4; // "YOUTH_1";
                ExecResult<YouthConfirmOrderResponseModel> rechargeResult = 
                    await DoRechargeByBrokerAsync(request, request.TelCharger, request.FaceAmount, tradeType, null, loyaltyYear, cancellationToken)
                    .ConfigureAwait(false);

                if (!rechargeResult.ExecStatus)
                {
                    return rechargeResult;
                }

                //ToDo Get From Redis
                decimal giftOffer = 100111;

                //decimal giftOffer = ResolveDynamicGiftOfferFromRedis(request.FaceAmount);
                //if (giftOffer < 0)
                //{
                //    return await BuildErrorAsync(DynamicGiftOfferErrorCode, "TOPUP_ERROR", cancellationToken).ConfigureAwait(false);
                //}

                GeneralHuawiResponse offeringResult = await _endpoint.ChangeSubscribersOfferingAdd(new ChangeSubscribersOfferingAddTcpRequest
                {
                    PrimaryIdentity = request.TelCharger,
                    OfferingId = giftOffer.ToString(),
                    BrokerId = request.SapId.ToString(),
                    SponserMsisdn = request.TelSponser.ToString(),
                    Mss = $"Topup/Topin{Guid.NewGuid()}"
                }).ConfigureAwait(false);

                if (!string.Equals(offeringResult.ResponseType, "0", StringComparison.Ordinal))
                {
                    decimal responseCode = ResponseCodeParser.ParseDecimalOrFallback(offeringResult.ResponseType, DynamicGiftOfferErrorCode);
                    string message = await _messageProvider.ResolveAsync(responseCode, offeringResult.ResponseDesc, cancellationToken).ConfigureAwait(false);
                    rechargeResult.ExecStatus = false;
                    rechargeResult.ResultCode = responseCode;
                    rechargeResult.ResultMessage = message;
                }

                return rechargeResult;
            }

            return await BuildErrorAsync(InternalErrorCode, "TOPUP_ERROR", cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            return await BuildErrorAsync(InternalErrorCode, "TOPUP_ERROR", cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task<ExecResult<YouthConfirmOrderResponseModel>> DoRechargeByBrokerAsync(YouthConfirmOrderRequestModel request,
                                                                                           string telCharger,
                                                                                           decimal faceAmount,
                                                                                           decimal tradeType,
                                                                                           string beId,
                                                                                           decimal loyaltyYear,
                                                                                           CancellationToken cancellationToken)
    {
        GeneralHuawiResponse response = await _endpoint.RechargeByBroker(new RechargeByBrokerTcpRequest
        {
            PrimaryIdentity = telCharger,
            Amount = (long)faceAmount,
            BrokerId = request.SapId.ToString(CultureInfo.InvariantCulture),
            RechargeChannelID = request.ChannelId.ToString(CultureInfo.InvariantCulture),
            TradeType = tradeType.ToString(CultureInfo.InvariantCulture),
            BeId = beId,
            Mss = $"Topup/Topin{Guid.NewGuid()}"
        }).ConfigureAwait(false);

        decimal code = ResponseCodeParser.ParseDecimalOrFallback(response.ResponseType, InternalErrorCode);
        string message = await _messageProvider.ResolveAsync(code, response.ResponseDesc, cancellationToken).ConfigureAwait(false);

        return new ExecResult<YouthConfirmOrderResponseModel>
        {
            ExecStatus = code == 0,
            ResultCode = code,
            ResultMessage = message,
            Data = new YouthConfirmOrderResponseModel
            {
                AccountName = request.CardNo,
                Remain = response.ResponseDesc,
                TradeType = tradeType,
                RechargeSerialNo = response.ResponseDesc,
                LoyaltyYear = loyaltyYear
            }
        };
    }

    private async Task<ExecResult<YouthConfirmOrderResponseModel>> BuildErrorAsync(decimal code, string defaultMessage, CancellationToken cancellationToken)
    {
        string message = await _messageProvider.ResolveAsync(code, defaultMessage, cancellationToken).ConfigureAwait(false);
        return new ExecResult<YouthConfirmOrderResponseModel>
        {
            ExecStatus = false,
            ResultCode = code,
            ResultMessage = message,
            Data = new YouthConfirmOrderResponseModel()
        };
    }

    private static decimal ResolveDynamicGiftOfferFromRedis(decimal faceAmount)
    {
        return -1;
    }
}
