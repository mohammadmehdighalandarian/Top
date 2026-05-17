using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using System.Globalization;
using TopinLite.Biz.ChargeSaleHandler.Charge;
using TopinLite.Biz.ChargeSaleHandler.Orders;
using TopinLite.Biz.ChargeSaleHandler.Sms;
using TopinLite.Biz.ChargeSaleHandler.Validation;
using TopinLite.Domain.Commons;
using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeSaleHandler.ServiceProviders
{
    public interface IChargeServiceProvider
    {
        Task<ExecResult<ChargeRequestOrderResponse>> RequestOrderAsync(ChargeRequestOrderRequest request, CancellationToken cancellationToken);

        Task<ExecResult<ChargeConfirmOrderResponse>> ConfirmOrderAsync(ChargeConfirmOrderRequest request, CancellationToken cancellationToken);
    }

    public class ChargeServiceProvider : IChargeServiceProvider
    {
        private readonly ICommonValidator _validator;
        private readonly IChargeTypeResolver _resolver;
        private readonly IRpcClient _rpc;
        private readonly IOrderStore _store;
        private readonly IDoChargeService _doCharge;
        private readonly ISendSmsService _sendSms;
        private readonly ILogger<ChargeServiceProvider> _logger;

        public ChargeServiceProvider(
            ICommonValidator validator,
            IChargeTypeResolver resolver,
            IRpcClient rpc,
            IOrderStore store,
            ILogger<ChargeServiceProvider> logger,
            IDoChargeService doCharge,
            ISendSmsService sendSms)
        {
            _validator = validator;
            _resolver = resolver;
            _rpc = rpc;
            _store = store;
            _logger = logger;
            _doCharge = doCharge;
            _sendSms = sendSms;
        }

        public async Task<ExecResult<ChargeRequestOrderResponse>> RequestOrderAsync(ChargeRequestOrderRequest request, CancellationToken cancellationToken)
        {
            ExecResult general = await _validator.ChargeRequestValidateAsync(request, cancellationToken).ConfigureAwait(false);

            if (!general.ExecStatus)
            {
                _logger.LogInformation("RequestOrder rejected by general validator: code={Code} msg={Message} broker={Broker}",
                    general.ResultCode, general.ResultMessage, request.BrokerId);
                return Fail<ChargeRequestOrderResponse>(general.ResultCode, general.ResultMessage);
            }

            (string ChargeType, string ValidateSubject)? routing = _resolver.Resolve(request.PayloadId);
            if (routing is null)
            {
                return Fail<ChargeRequestOrderResponse>(ResultCodes.BadInput.Code, ResultCodes.BadInput.Message);
            }

            ChargeTypeValidateRequest typeRequest = new()
            {
                TelNum = request.TelNum,
                TelGift = request.TelGift,
                Amount = request.Amount,
                PayloadId = request.PayloadId,
                ChannelId = request.ChannelId,
                BrokerId = request.BrokerId,
                CustomerId = request.CustomerId,
                VendorId = request.VendorId,
                AdditionalData = request.AdditionalData
            };

            ChargeTypeValidateResponse? typeResult;
            try
            {
                RpcResult<ChargeTypeValidateResponse> rpcResult = await _rpc
                    .RequestAsync<ChargeTypeValidateRequest, ChargeTypeValidateResponse>(
                        subject: routing.Value.ValidateSubject,
                        request: typeRequest,
                        options: new RpcCallOptions { Timeout = TimeSpan.FromSeconds(5) },
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                typeResult = rpcResult.Success ? rpcResult.Data : null;
                if (!rpcResult.Success || typeResult is null)
                {
                    _logger.LogWarning("Per-type validator '{Subject}' replied not-success: {Code}/{Message}",
                        routing.Value.ValidateSubject, rpcResult.Code, rpcResult.Message);

                    decimal code = decimal.TryParse(rpcResult.Code, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed)
                        ? parsed
                        : ResultCodes.BadInput.Code;
                    return Fail<ChargeRequestOrderResponse>(code, rpcResult.Message!);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RPC to '{Subject}' threw — treating as transient failure",
                    routing.Value.ValidateSubject);
                return Fail<ChargeRequestOrderResponse>(ResultCodes.BadInput.Code, "Type validator unavailable.");
            }

            if (!typeResult.ExecStatus || typeResult.ResultCode != 0)
            {
                return Fail<ChargeRequestOrderResponse>(typeResult.ResultCode, typeResult.ResultMessage);
            }

            // 4. Mint OrderId from Redis (INCR).
            decimal orderId = await _store.NextOrderIdAsync(cancellationToken).ConfigureAwait(false);

            // 5. Persist OrderContext (with Youth fields if relevant).
            var ctx = new OrderContext
            {
                PkSeqPinlessCharge = orderId,
                TelNum = request.TelNum,
                FkTelCharger = request.TelGift,
                ChargeAmount = decimal.TryParse(request.Amount, out decimal amt) ? amt : 0,
                FkBrokerId = decimal.TryParse(request.BrokerId, out decimal bid) ? bid : 0,
                SapId = decimal.TryParse(request.BrokerId, out decimal sapId) ? sapId : 0, //TODO SapId
                ChannelId = decimal.TryParse(request.ChannelId, out decimal chId) ? chId : 0,
                OfferCode = decimal.TryParse(request.PayloadId, out decimal oc) ? oc : 0,
                ChargeStatus = 0,
                ChargeType = routing.Value.ChargeType,
                PayloadId = request.PayloadId,
                InsDate = GetPersianDate(),
                InsTime = DateTime.Now.ToString("HH:mm:ss"),
                CreatedAt = DateTimeOffset.UtcNow

            };

            await _store.SaveAsync(ctx, cancellationToken).ConfigureAwait(false);

            _logger.LogInformation("RequestOrder accepted: orderId={OrderId} type={Type} broker={Broker}",
                orderId, routing.Value.ChargeType, request.BrokerId);

            return Ok(new ChargeRequestOrderResponse { OrderId = orderId });
        }

        public async Task<ExecResult<ChargeConfirmOrderResponse>> ConfirmOrderAsync(ChargeConfirmOrderRequest request, CancellationToken cancellationToken)
        {
            // ── Step 1: all validations ───────────────────────────────────────────
            ExecResult validation = await _validator
                .ChargeConfirmValidateAsync(request, cancellationToken)
                .ConfigureAwait(false);

            if (!validation.ExecStatus)
            {
                _logger.LogInformation(
                    "ConfirmOrder rejected: orderId={OrderId} code={Code} msg={Msg}",
                    request.OrderId, validation.ResultCode, validation.ResultMessage);

                return Fail<ChargeConfirmOrderResponse>(validation.ResultCode, validation.ResultMessage);
            }

            // ── Step 2: reload context (validator already confirmed it exists) ────
            OrderContext ctx = (await _store.TryGetAsync(request.OrderId, cancellationToken).ConfigureAwait(false))!;

            // Mark in-progress so concurrent retries are blocked
            ctx.ChargeStatus = 2;
            await _store.SaveAsync(ctx, cancellationToken).ConfigureAwait(false);

            // ── Step 3: execute the charge ────────────────────────────────────────
            var doChargeReq = new DoChargeRequest
            {
                TelCharger = ctx.FkTelCharger.ToString("0", CultureInfo.InvariantCulture),
                FaceAmount = ctx.ChargeAmount,
                ExtraChargeOfferId = ctx.FkChargeType,
                // Pass existing SeqCrm when this is a retry after a -9888 response
                SeqCrm = ctx.ResponseType == -9888 ? (long)ctx.FkCrmSeq : 0,
                ProviderId = ctx.PkSeqPinlessCharge,
                BrokerId = ctx.FkBrokerId,
                SapId = ctx.SapId,
                ChannelId = ctx.ChannelId,
                AccountId = ctx.FkAccounts,
                TelSponsor = ctx.TelNum
            };

            DoChargeResult chargeResult = await _doCharge
                .ExecuteAsync(doChargeReq, cancellationToken)
                .ConfigureAwait(false);

            // ── Step 4a: charge succeeded ─────────────────────────────────────────
            if (chargeResult.Success)
            {
                ctx.ChargeStatus = 1;
                ctx.FkBank = decimal.TryParse(request.BankCode, out decimal bk) ? bk : 0;
                ctx.Rrn = request.RRN.ToString(CultureInfo.InvariantCulture);
                ctx.IdCardNo = request.CardNo;   // encrypt before storing if required
                ctx.IdCardType = short.TryParse(request.CardType, out short ct2) ? ct2 : (short)0;
                ctx.Retry = ctx.Retry + 1;
                ctx.RechargeSerialNo = chargeResult.RechargeSerialNo;
                ctx.FkCrmSeq = chargeResult.SeqCrm;
                ctx.ChargeTimestamp = DateTimeOffset.UtcNow;
                ctx.ChargeDate = GetPersianDate();
                ctx.ChargeTime = DateTime.Now.ToString("HH:mm:ss");

                await _store.SaveAsync(ctx, cancellationToken).ConfigureAwait(false);

                // TODO Broker sale-amount update
                //_ = FireBrokerSaleAmountAsync(ctx, cancellationToken);

                // SMS — best-effort, non-blocking
                _ = _sendSms.SendAsync(new SendSmsRequest
                {
                    TelNum = ctx.TelNum,
                    TelCharger = ctx.FkTelCharger,
                    Amount = ctx.ChargeAmount,
                    CardCharge = ctx.ChargeAmount,
                    ExtraChargeId = ctx.FkChargeType,
                    BrokerId = ctx.FkBrokerId,
                    LoyaltyYear = chargeResult.LoyaltyYear,
                    ProviderId = ctx.PkSeqPinlessCharge,
                    OfferCode = ctx.OfferCode
                }, CancellationToken.None);

                _logger.LogInformation(
                    "ConfirmOrder SUCCESS: orderId={OrderId} type={Type} serial={Serial}",
                    request.OrderId, ctx.ChargeType, chargeResult.RechargeSerialNo);

                return Ok(new ChargeConfirmOrderResponse
                {
                    AccountName = ctx.TelNum.ToString("0", CultureInfo.InvariantCulture),
                    Remain = string.Empty,
                    OfferCode = (int)ctx.OfferCode,
                    LoyaltyYear = chargeResult.LoyaltyYear,
                    TradeType = chargeResult.TradeType,
                    RechargeSerialNo = chargeResult.RechargeSerialNo,
                    ResultRaw = chargeResult.ResultMessage
                });
            }

            // ── Step 4b: charge failed ────────────────────────────────────────────
            // Oracle: UPDATE … SET CHARGE_STATUS=-1, RETRY=RETRY+1, RESPONSE_TYPE=…
            ctx.ChargeStatus = -1;
            ctx.ResponseType = chargeResult.ResultCode;
            ctx.ResponseDesc = chargeResult.ResultMessage;
            ctx.Retry = ctx.Retry + 1;
            ctx.UpdDate = GetPersianDate();
            ctx.UpdTime = DateTime.Now.ToString("HH:mm:ss");
            ctx.ChargeTimestamp = DateTimeOffset.UtcNow;
            ctx.FkCrmSeq = chargeResult.SeqCrm;

            await _store.SaveAsync(ctx, cancellationToken).ConfigureAwait(false);

            // Oracle: IF P_RESPONSE_TYPE = 118100446 THEN P_RESPONSE_TYPE := -1046
            decimal resultCode = chargeResult.ResultCode == 118100446m ? -1046m : chargeResult.ResultCode;

            _logger.LogInformation(
                "ConfirmOrder FAILED: orderId={OrderId} code={Code} msg={Msg}",
                request.OrderId, resultCode, chargeResult.ResultMessage);

            return Fail<ChargeConfirmOrderResponse>(resultCode, chargeResult.ResultMessage);
        }

        private async Task FireBrokerSaleAmountAsync(OrderContext ctx, CancellationToken ct)
        {
            try
            {
                //await _rpc.RequestAsync<BrokerSaleAmountUpdateRequest, BrokerSaleAmountUpdateResponse>(
                //    subject: "infra.broker-sale-amount.update",
                //    request: new BrokerSaleAmountUpdateRequest
                //    {
                //        BrokerId = ctx.FkBrokerId,
                //        OfferId = ctx.FkChargeType,
                //        AccountId = ctx.FkAccounts,
                //        SaleAmount = ctx.ChargeAmount,
                //        SaleDate = GetPersianDate()
                //    },
                //    options: new RpcCallOptions { Timeout = TimeSpan.FromSeconds(5) },
                //    cancellationToken: ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "FireBrokerSaleAmount: NATS call failed (non-fatal)");
            }
        }

        private static string GetPersianDate()
        {
            var pc = new PersianCalendar();
            DateTime now = DateTime.Now;
            return $"{pc.GetYear(now):0000}/{pc.GetMonth(now):00}/{pc.GetDayOfMonth(now):00}";
        }

        private static ExecResult<T> Ok<T>(T data) where T : new()
            => new() { ExecStatus = true, ResultCode = 0, ResultMessage = "Success", Data = data };

        private static ExecResult<T> Fail<T>(decimal code, string message) where T : new()
            => new() { ExecStatus = false, ResultCode = code, ResultMessage = message, Data = new T() };
    }
}