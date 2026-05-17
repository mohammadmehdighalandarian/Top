using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using System.Globalization;
using TopinLite.Domain.Commons;
using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeSaleHandler.Charge
{
    public sealed class DoChargeRequest
    {
        public string TelCharger { get; init; } = string.Empty; // FK_TEL_CHARGER
        public decimal FaceAmount { get; init; }                  // P_FACE_AMOUNT
        public decimal ExtraChargeOfferId { get; init; }                  // P_EXTRA_CHARGE  (PK_OFFERS)
        public long SeqCrm { get; init; }                  // P_SEQ_CRM  (IN OUT)
        public decimal ProviderId { get; init; }
        public decimal BrokerId { get; init; }
        public decimal SapId { get; init; }
        public decimal ChannelId { get; init; }
        public decimal AccountId { get; init; }
        public decimal TelSponsor { get; init; }                  // P_TEL_SPONSER (FK_TEL_NUM#)
    }

    public sealed class DoChargeResult
    {
        public bool Success { get; init; }
        public decimal ResultCode { get; init; }
        public string ResultMessage { get; init; } = string.Empty;
        public string TradeType { get; init; } = string.Empty;
        public string RechargeSerialNo { get; init; } = string.Empty;
        public long SeqCrm { get; init; }
        public int LoyaltyYear { get; init; }
    }

    public interface IDoChargeService
    {
        Task<DoChargeResult> ExecuteAsync(DoChargeRequest request, CancellationToken cancellationToken);
    }

    public sealed class DoChargeService : IDoChargeService
    {
        // ── NATS subjects ────────────────────────────────────────────────────────
        private const string SubjectOffers = "infra.cache.offers";
        private const string SubjectWllPrefixes = "infra.cache.wll-prefixes";
        private const string SubjectDynamicConditions = "infra.cache.dynamic-conditions";
        private const string SubjectBrokerGiftPct = "infra.cache.broker-gift-percent";
        private const string SubjectTradeType = "infra.cache.trade-type";
        private const string SubjectRecharge = "crm.rechargebybroker";
        private const string SubjectQuerySubscriber = "crm.querysubscriber";
        private const string SubjectOfferingAdd = "crm.changesubscribersofferingadd";

        private const decimal OfferDirect = 1001m;
        private const decimal OfferDiy = 1002m;
        private const decimal OfferExtra = 1003m;
        private const decimal OfferYouth = 1004m;
        private const decimal OfferWomen = 1005m;
        private const decimal OfferLoyalty = 1006m;

        private readonly IRpcClient _rpc;
        private readonly ILogger<DoChargeService> _logger;

        public DoChargeService(IRpcClient rpc, ILogger<DoChargeService> logger)
        {
            _rpc = rpc;
            _logger = logger;
        }

        public async Task<DoChargeResult> ExecuteAsync(DoChargeRequest req, CancellationToken ct)
        {
            try
            {
                return await RunAsync(req, ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DoCharge: unhandled exception for provider={ProviderId}", req.ProviderId);
                return Fail(ResultCodes.BadInput.Code, ResultCodes.BadInput.Message);
            }
        }

        private async Task<DoChargeResult> RunAsync(DoChargeRequest req, CancellationToken ct)
        {
            OfferResponseModel? offer = await TryRpcAsync<OfferRequestModel, OfferResponseModel>(
                SubjectOffers,
                new OfferRequestModel { OfferCode = req.ExtraChargeOfferId },
                ct).ConfigureAwait(false);

            if (offer is null)
            {
                _logger.LogWarning("DoCharge: offer PK={OfferId} not found", req.ExtraChargeOfferId);
                return Fail(ResultCodes.OfferInactive.Code, ResultCodes.OfferInactive.Message);
            }

            decimal offerCode = offer.OfferCode;

            (bool isWll, string beid) = await ResolveWllAsync(req.TelCharger, ct).ConfigureAwait(false);

            decimal faceAmount = await ComputeFaceAmountAsync(req, ct).ConfigureAwait(false);

            return offerCode switch
            {
                OfferDirect or OfferDiy =>  await HandleDirectDiyAsync(req, faceAmount, isWll, beid, ct).ConfigureAwait(false),

                OfferExtra => await HandleExtraChargeAsync(req, faceAmount, ct).ConfigureAwait(false),

                OfferYouth => await HandleYouthChargeAsync(req, faceAmount, ct).ConfigureAwait(false),

                OfferWomen => await HandleWomenChargeAsync(req, faceAmount, ct).ConfigureAwait(false),

                OfferLoyalty => await HandleLoyaltyChargeAsync(req, faceAmount, ct).ConfigureAwait(false),

                _ => Fail(ResultCodes.BadInput.Code, $"Unknown offer code {offerCode}.")
            };
        }

        private async Task<(bool IsWll, string Beid)> ResolveWllAsync(string telCharger, CancellationToken ct)
        {
            if (telCharger.Length < 4)
                return (false, "10101");

            string prefix = telCharger[..4];
            if (!int.TryParse(prefix, out int prefixInt))
                return (false, "10101");

            WllPrefixesResponseModel? wll = await TryRpcAsync<WllPrefixesRequestModel, WllPrefixesResponseModel>(
                SubjectWllPrefixes,
                new WllPrefixesRequestModel { PkWllPrefix = prefixInt },
                ct).ConfigureAwait(false);

            if (wll is null)
                return (false, "10101");

            return (true, wll.Beid?.ToString() ?? "10101");
        }

        private async Task<decimal> ComputeFaceAmountAsync(DoChargeRequest req, CancellationToken ct)
        {
            BrokerGiftPercentResponseModel? gift = await TryRpcAsync<BrokerGiftPercentRequestModel, BrokerGiftPercentResponseModel>(
                SubjectBrokerGiftPct,
                new BrokerGiftPercentRequestModel { BrokerId = req.BrokerId, OfferId = req.ExtraChargeOfferId },
                ct).ConfigureAwait(false);

            if (gift is null)
                return req.FaceAmount;

            decimal pct = gift.GiftPercent;
            if (req.AccountId != 1 && pct > 5)
                pct = 5;

            return Math.Round(req.FaceAmount * (100 + pct) / 100);
        }

        private async Task<DoChargeResult> HandleDirectDiyAsync(DoChargeRequest req, decimal faceAmount, bool isWll, string beid, CancellationToken ct)
        {
            string? tradeType = await ResolveTradeTypeAsync("MCI111", ct).ConfigureAwait(false);
            if (tradeType is null)
                return Fail(ResultCodes.BadInput.Code, "Trade type MCI111 not found.");

            // Oracle: IF V_IS_WLL = 1 → pass BEID, else omit it
            RechargeByBrokerRequestModel rechargeReq = BuildRechargeRequest(
                req, faceAmount, tradeType, beId: isWll ? beid : "0");

            RechargeByBrokerRichResponse? resp = await CallRechargeAsync(rechargeReq, ct).ConfigureAwait(false);
            if (resp is null)
                return Fail(ResultCodes.BadInput.Code, "CRM recharge call failed.");

            return MapRechargeResponse(resp, tradeType, loyaltyYear: 0);
        }

        private async Task<DoChargeResult> HandleExtraChargeAsync(DoChargeRequest req, decimal faceAmount, CancellationToken ct)
        {
            string? tradeType = await ResolveTradeTypeAsync("EXTRA_CHARGE", ct).ConfigureAwait(false);
            if (tradeType is null)
                return Fail(ResultCodes.BadInput.Code, "Trade type EXTRA_CHARGE not found.");

            RechargeByBrokerRichResponse? resp = await CallRechargeAsync(
                BuildRechargeRequest(req, faceAmount, tradeType, beId: "0"), ct).ConfigureAwait(false);

            if (resp is null)
                return Fail(ResultCodes.BadInput.Code, "CRM recharge call failed.");

            return MapRechargeResponse(resp, tradeType, loyaltyYear: 0);
        }

        private async Task<DoChargeResult> HandleYouthChargeAsync(DoChargeRequest req, decimal faceAmount, CancellationToken ct)
        {
            string? tradeType = await ResolveTradeTypeAsync("YOUTH_1", ct).ConfigureAwait(false);
            if (tradeType is null)
                return Fail(ResultCodes.BadInput.Code, "Trade type YOUTH_1 not found.");

            RechargeByBrokerRichResponse? resp = await CallRechargeAsync(
                BuildRechargeRequest(req, faceAmount, tradeType, beId: "0"), ct).ConfigureAwait(false);

            if (resp is null)
                return Fail(ResultCodes.BadInput.Code, "CRM recharge call failed.");

            DoChargeResult result = MapRechargeResponse(resp, tradeType, loyaltyYear: 0);

            if (result.Success)
            {
                DynamicConditionsResponseModel? dc = await TryRpcAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
                    SubjectDynamicConditions,
                    new DynamicConditionsRequestModel
                    {
                        Biztype = "124",
                        KeyStr = faceAmount.ToString("0", CultureInfo.InvariantCulture)
                    },
                    ct).ConfigureAwait(false);

                if (dc is not null &&
                    decimal.TryParse(dc.ValueStr, out decimal giftOfferId) &&
                    giftOfferId > 0)
                {
                    // Oracle: PRC_SUBSCRIBER_OFFERING_ADD — non-fatal, fire-and-forget
                    _ = FireOfferingAddAsync(req, giftOfferId, resp.SeqCrm, ct);
                }
                else
                {
                    _logger.LogWarning(
                        "DoCharge/Youth: no gift offer found in dynamic-conditions for amount={Amount} (non-fatal)",
                        faceAmount);
                }
            }

            return result;
        }

        private async Task<DoChargeResult> HandleWomenChargeAsync(DoChargeRequest req, decimal faceAmount, CancellationToken ct)
        {
            string? tradeType = await ResolveTradeTypeAsync("persian", ct).ConfigureAwait(false);
            if (tradeType is null)
                return Fail(ResultCodes.BadInput.Code, "Trade type 'persian' not found.");

            RechargeByBrokerRichResponse? resp = await CallRechargeAsync(
                BuildRechargeRequest(req, faceAmount, tradeType, beId: "0"), ct).ConfigureAwait(false);

            if (resp is null)
                return Fail(ResultCodes.BadInput.Code, "CRM recharge call failed.");

            return MapRechargeResponse(resp, tradeType, loyaltyYear: 0);
        }

        private async Task<DoChargeResult> HandleLoyaltyChargeAsync(DoChargeRequest req, decimal faceAmount, CancellationToken ct)
        {
            (string? loyaltyName, int loyaltyYear) = await GetLoyaltyYearAsync(req.TelCharger, ct).ConfigureAwait(false);

            if (loyaltyName is null || loyaltyYear == 0)
            {
                _logger.LogInformation(
                    "DoCharge/Loyalty: subscriber {Tel} not eligible (year={Year})",
                    req.TelCharger, loyaltyYear);
                return Fail(ResultCodes.LoyaltyChargeRestriction.Code, ResultCodes.LoyaltyChargeRestriction.Message);
            }

            // Trade-type name IS the loyalty name (e.g. "LOYALTY_3") — matches Oracle CASE block
            string? tradeType = await ResolveTradeTypeAsync(loyaltyName, ct).ConfigureAwait(false);
            if (tradeType is null)
                return Fail(ResultCodes.BadInput.Code, $"Trade type '{loyaltyName}' not found.");

            RechargeByBrokerRichResponse? resp = await CallRechargeAsync(
                BuildRechargeRequest(req, faceAmount, tradeType, beId: "0"), ct).ConfigureAwait(false);

            if (resp is null)
                return Fail(ResultCodes.BadInput.Code, "CRM recharge call failed.");

            return MapRechargeResponse(resp, tradeType, loyaltyYear);
        }

        private async Task<(string? LoyaltyName, int Year)> GetLoyaltyYearAsync(string telNum, CancellationToken ct)
        {
            try
            {
                QuerySubscriberResponseModel? sub = await TryRpcAsync<QuerySubscriberRequestModel, QuerySubscriberResponseModel>(
                    SubjectQuerySubscriber,
                    new QuerySubscriberRequestModel
                    {
                        PrimaryIdentity = telNum,
                        Mss = $"Topup/Topin{Guid.NewGuid():N}"
                    },
                    ct).ConfigureAwait(false);

                if (sub is null || sub.ResponseType != "0" || sub.ResponseDesc is null)
                    return (null, 0);

                // Oracle: FNC_GET_TOKEN(V_RESPONSE_DESC, 8, ';')
                string activeDateStr = TokenParser.GetToken(sub.ResponseDesc, 8, ";");

                // Oracle format: 'MM/DD/YYYY HH24:MI:SS'
                if (!DateParser.TryParse(activeDateStr, out DateTime activeDate, DateParser.SubscriberDateFormats))
                    return (null, 0);

                int months = DiffMonths(DateTime.UtcNow, activeDate);

                // Oracle: IF V_MONTH < 12 THEN P_LOYALTY := NULL; RETURN; END IF;
                if (months < 12)
                    return (null, 0);

                int years = Math.Min(months / 12, 10);

                string loyaltyName = $"LOYALTY_{years}";
                return (loyaltyName, years);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLoyaltyYear: unexpected error for tel={Tel}", telNum);
                return (null, 0);
            }
        }

        private RechargeByBrokerRequestModel BuildRechargeRequest(DoChargeRequest req, decimal faceAmount, string tradeType, string beId)
            => new()
            {
                PrimaryIdentity = req.TelCharger,
                Amount = faceAmount,
                BrokerId = req.SapId.ToString("0", CultureInfo.InvariantCulture),
                RechargeChannelID = req.ChannelId.ToString("0", CultureInfo.InvariantCulture),
                TradeType = tradeType,
                BeId = beId,
                Mss = $"Topup/Topin{Guid.NewGuid():N}"
            };

        private async Task<RechargeByBrokerRichResponse?> CallRechargeAsync(RechargeByBrokerRequestModel request, CancellationToken ct)
        {
            try
            {
                RpcResult<RechargeByBrokerRichResponse> result = await _rpc
                    .RequestAsync<RechargeByBrokerRequestModel, RechargeByBrokerRichResponse>(
                        subject: SubjectRecharge,
                        request: request,
                        options: new RpcCallOptions { Timeout = TimeSpan.FromSeconds(15) },
                        cancellationToken: ct)
                    .ConfigureAwait(false);

                return result.Success ? result.Data : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CallRecharge: NATS call threw");
                return null;
            }
        }

        private static DoChargeResult MapRechargeResponse(RechargeByBrokerRichResponse resp, string tradeType, int loyaltyYear)
        {
            bool ok = resp.ResponseType == "0";
            decimal code = decimal.TryParse(resp.ResponseType, out decimal d) ? d : -1m;

            return new DoChargeResult
            {
                Success = ok,
                ResultCode = ok ? 0m : code,
                ResultMessage = resp.ResponseDesc ?? string.Empty,
                TradeType = tradeType,
                RechargeSerialNo = resp.RechargeSerialNo ?? string.Empty,
                SeqCrm = resp.SeqCrm,
                LoyaltyYear = loyaltyYear
            };
        }

        private async Task FireOfferingAddAsync(DoChargeRequest req, decimal giftOfferId, long seqCrm, CancellationToken ct)
        {
            try
            {
                await _rpc.RequestAsync<ChangeSubscribersOfferingAddRequestModel, ChangeSubscribersOfferingAddResponseModel>(
                    subject: SubjectOfferingAdd,
                    request: new ChangeSubscribersOfferingAddRequestModel
                    {
                        PrimaryIdentity = req.TelCharger,
                        OfferingId = giftOfferId.ToString("0", CultureInfo.InvariantCulture),
                        BrokerId = req.SapId.ToString("0", CultureInfo.InvariantCulture),
                        SponserMsisdn = req.TelSponsor.ToString("0", CultureInfo.InvariantCulture),
                        Mss = $"Topup/Topin{Guid.NewGuid():N}"
                    },
                    options: new RpcCallOptions { Timeout = TimeSpan.FromSeconds(10) },
                    cancellationToken: ct)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "DoCharge/Youth: offering-add failed (non-fatal)");
            }
        }

        private async Task<string?> ResolveTradeTypeAsync(string operatorName, CancellationToken ct)
        {
            TradeTypeResponseModel? tt = await TryRpcAsync<TradeTypeRequestModel, TradeTypeResponseModel>(
                SubjectTradeType,
                new TradeTypeRequestModel { OperatorName = operatorName },
                ct).ConfigureAwait(false);

            return tt?.OperatorId.ToString(CultureInfo.InvariantCulture);
        }

        private async Task<TResp?> TryRpcAsync<TReq, TResp>(string subject, TReq request, CancellationToken ct)
            where TReq : class
            where TResp : class
        {
            try
            {
                RpcResult<TResp> result = await _rpc.RequestAsync<TReq, TResp>(
                    subject: subject,
                    request: request,
                    options: new RpcCallOptions { Timeout = TimeSpan.FromSeconds(5) },
                    cancellationToken: ct)
                    .ConfigureAwait(false);

                return result.Success ? result.Data : null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "DoCharge.TryRpc: call to '{Subject}' threw", subject);
                return null;
            }
        }

        private static DoChargeResult Fail(decimal code, string message)
            => new() { Success = false, ResultCode = code, ResultMessage = message };

        private static int DiffMonths(DateTime newer, DateTime older)
        {
            if (newer < older)
                return 0;

            int months = (newer.Year - older.Year) * 12 + newer.Month - older.Month;
            if (newer.Day < older.Day)
                months--;

            return Math.Max(0, months);
        }

        public sealed class RechargeByBrokerRichResponse : GeneralHuawiResponse
        {
            public long SeqCrm { get; init; }
            public string? RechargeSerialNo { get; init; }
        }

        public sealed class BrokerGiftPercentRequestModel
        {
            public decimal BrokerId { get; init; }
            public decimal OfferId { get; init; }
        }

        public sealed class BrokerGiftPercentResponseModel
        {
            public decimal GiftPercent { get; init; }
        }

        public sealed class TradeTypeRequestModel
        {
            public string OperatorName { get; init; } = string.Empty;
        }

        public sealed class TradeTypeResponseModel
        {
            public decimal OperatorId { get; init; }
            public string OperatorName { get; init; } = string.Empty;
        }
    }
}
