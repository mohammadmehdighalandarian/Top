using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using System.Globalization;
using TopinLite.Domain.Commons;
using TopinLite.Domain.Messaging;
using TopinLite.Services.Commons;
using static TopinLite.Biz.ChargeSaleHandler.Charge.DoChargeService;

namespace TopinLite.Biz.ChargeSaleHandler.Sms
{
    public sealed class SendSmsRequest
    {
        public decimal TelNum { get; init; }
        public decimal TelCharger { get; init; }
        public decimal Amount { get; init; }
        public decimal CardCharge { get; init; }
        public decimal ExtraChargeId { get; init; }
        public decimal BrokerId { get; init; }
        public int LoyaltyYear { get; init; }
        public decimal ProviderId { get; init; }
        public decimal OfferCode { get; init; }
    }

    public interface ISendSmsService
    {
        Task SendAsync(SendSmsRequest request, CancellationToken cancellationToken);
    }

    public sealed class SendSmsService : ISendSmsService
    {
        private const decimal OfferDirect = 1001m;
        private const decimal OfferDiy = 1002m;
        private const decimal OfferExtra = 1003m;
        private const decimal OfferYouth = 1004m;
        private const decimal OfferWomen = 1005m;
        private const decimal OfferLoyalty = 1006m;

        private readonly IRpcClient _rpc;
        private readonly IPardisSmsProvider _smsProvider;
        private readonly ILogger<SendSmsService> _logger;

        public SendSmsService(IRpcClient rpc, IPardisSmsProvider smsProvider, ILogger<SendSmsService> logger)
        {
            _rpc = rpc;
            _smsProvider = smsProvider;
            _logger = logger;
        }

        public async Task SendAsync(SendSmsRequest req, CancellationToken ct)
        {
            try
            {
                await RunAsync(req, ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // SMS is fire-and-forget; swallow and log.
                _logger.LogError(ex, "SendSms: unhandled exception for provider={ProviderId}", req.ProviderId);
            }
        }

        private async Task RunAsync(SendSmsRequest req, CancellationToken ct)
        {
            decimal faceAmount = await ResolveFaceAmountAsync(req, ct).ConfigureAwait(false);

            string brokerAddendum = await ResolveBrokerSmsAddendumAsync(req.BrokerId, req.ExtraChargeId, req.OfferCode, req.TelNum, req.TelCharger, ct)
                .ConfigureAwait(false);

            switch (req.OfferCode)
            {
                case OfferDirect:
                case OfferDiy:
                    await SendDirectDiyAsync(req, faceAmount, brokerAddendum, ct).ConfigureAwait(false);
                    break;

                case OfferExtra:
                    await SendExtraChargeAsync(req, brokerAddendum, ct).ConfigureAwait(false);
                    break;

                case OfferWomen:
                    await SendWomenChargeAsync(req, brokerAddendum, ct).ConfigureAwait(false);
                    break;

                case OfferYouth:
                    await SendYouthChargeAsync(req, brokerAddendum, ct).ConfigureAwait(false);
                    break;

                case OfferLoyalty:
                    await SendLoyaltyChargeAsync(req, brokerAddendum, ct).ConfigureAwait(false);
                    break;

                default:
                    _logger.LogWarning("SendSms: unknown offer code {OfferCode}, skipping SMS", req.OfferCode);
                    break;
            }
        }

        private async Task SendDirectDiyAsync(SendSmsRequest req, decimal faceAmount, string addendum, CancellationToken ct)
        {
            string now = DateTime.Now.ToString("HH:mm:ss");
            string date = GetPersianDate();

            if (req.TelNum == req.TelCharger)
            {
                string text = await BuildTemplateAsync(100001, ct).ConfigureAwait(false);
                text = text
                    .Replace("%V_FACE_AMOUNT%", faceAmount.ToString("0", CultureInfo.InvariantCulture))
                    .Replace("%V_TIME%", now)
                    .Replace("%V_DATE%", date);

                await _smsProvider.SendSms(req.TelNum.ToString(), text + " " + addendum).ConfigureAwait(false);
            }
            else
            {
                string textToSponsor = await BuildTemplateAsync(100002, ct).ConfigureAwait(false);
                textToSponsor = textToSponsor
                    .Replace("%V_FACE_AMOUNT%", faceAmount.ToString("0", CultureInfo.InvariantCulture))
                    .Replace("%P_FK_TEL_CHARGER%", req.TelCharger.ToString())
                    .Replace("%V_TIME%", now)
                    .Replace("%V_DATE%", date);

                await _smsProvider.SendSms(req.TelNum.ToString(), textToSponsor + " " + addendum).ConfigureAwait(false);

                string textToCharged = await BuildTemplateAsync(100003, ct).ConfigureAwait(false);
                textToCharged = textToCharged
                    .Replace("%V_FACE_AMOUNT%", faceAmount.ToString("0", CultureInfo.InvariantCulture))
                    .Replace("%P_TEL_NUM%", req.TelNum.ToString())
                    .Replace("%V_TIME%", now)
                    .Replace("%V_DATE%", date);

                await _smsProvider.SendSms(req.TelCharger.ToString(), textToCharged).ConfigureAwait(false);
            }
        }

        private async Task SendExtraChargeAsync(SendSmsRequest req, string addendum, CancellationToken ct)
        {
            var data = StaticExtraCharge.Items.FirstOrDefault(x => x.ChargeAmount == req.CardCharge);

            if (data is null)
            {
                _logger.LogWarning("SendSms/Extra: no extra-charge data for cardAmount={Amount}", req.CardCharge);
                return;
            }

            string now = DateTime.Now.ToString("HH:mm:ss");
            string date = GetPersianDate();
            string exDate = GetPersianDatePlusDays(data.ExpiryTime);

            if (req.TelNum == req.TelCharger)
            {
                string text = await BuildTemplateAsync(1113, ct).ConfigureAwait(false);
                text = text
                    .Replace("%CHARGE_AMOUNT%", req.Amount.ToString("0", CultureInfo.InvariantCulture))
                    .Replace("%EXTRA_CHARGE_AMOUNT%", data.ExtraChargeAmount.ToString())
                    .Replace("%CUR_TIME%", now)
                    .Replace("%CUR_DATE%", date)
                    .Replace("%END_TIME%", now)
                    .Replace("%END_DATE%", exDate);

                await _smsProvider.SendSms(req.TelNum.ToString(), text + addendum).ConfigureAwait(false);
            }
            else
            {
                string textToSponsor = await BuildTemplateAsync(1112, ct).ConfigureAwait(false);
                textToSponsor = textToSponsor
                    .Replace("%CHARGE_AMOUNT_PLUS_EXTRA%", (req.Amount + data.ExtraChargeAmount).ToString("0", CultureInfo.InvariantCulture))
                    .Replace("%CHARGED_TEL%", req.TelCharger.ToString())
                    .Replace("%CUR_TIME%", now)
                    .Replace("%CUR_DATE%", date);

                await _smsProvider.SendSms(req.TelNum.ToString(), textToSponsor + addendum).ConfigureAwait(false);

                string textToCharged = await BuildTemplateAsync(1111, ct).ConfigureAwait(false);
                textToCharged = textToCharged
                    .Replace("%CHARGE_AMOUNT%", req.Amount.ToString("0", CultureInfo.InvariantCulture))
                    .Replace("%EXTRA_CHARGE_AMOUNT%", data.ExtraChargeAmount.ToString())
                    .Replace("%TEL_NUM%", req.TelNum.ToString())
                    .Replace("%CUR_TIME%", now)
                    .Replace("%CUR_DATE%", date)
                    .Replace("%END_TIME%", now)
                    .Replace("%END_DATE%", exDate);

                await _smsProvider.SendSms(req.TelCharger.ToString(), textToCharged).ConfigureAwait(false);
            }
        }

        private async Task SendWomenChargeAsync(SendSmsRequest req, string addendum, CancellationToken ct)
        {
            var data = StaticWomanCharge.Items.FirstOrDefault(x => x.CardAmount == req.CardCharge);

            if (data is null)
            {
                _logger.LogWarning("SendSms/Women: no women-charge data for cardAmount={Amount}", req.CardCharge);
                return;
            }

            string exDate = GetPersianDatePlusDays(data.ExpiryTime);

            if (req.TelNum == req.TelCharger)
            {
                string text = await BuildTemplateAsync(100004, ct).ConfigureAwait(false);
                text = text.Replace("%V_DATE%", exDate);
                await _smsProvider.SendSms(req.TelNum.ToString(), text + " " + addendum).ConfigureAwait(false);
            }
            else
            {
                string textToSponsor = await BuildTemplateAsync(100005, ct).ConfigureAwait(false);
                textToSponsor = textToSponsor
                    .Replace("%P_FK_TEL_CHARGER%", req.TelCharger.ToString())
                    .Replace("%V_DATE%", exDate);
                await _smsProvider.SendSms(req.TelNum.ToString(), textToSponsor + " " + addendum).ConfigureAwait(false);

                string textToCharged = await BuildTemplateAsync(100006, ct).ConfigureAwait(false);
                textToCharged = textToCharged.Replace("%P_TEL_NUM%", req.TelNum.ToString());
                await _smsProvider.SendSms(req.TelCharger.ToString(), textToCharged).ConfigureAwait(false);
            }
        }

        private async Task SendYouthChargeAsync(SendSmsRequest req, string addendum, CancellationToken ct)
        {
            var data = StaticYouthCharge.Items.FirstOrDefault(x => x.ChargeAmount == req.CardCharge);

            if (data is null)
            {
                _logger.LogWarning("SendSms/Youth: no youth-charge data for amount={Amount}", req.Amount);
                return;
            }

            int packageMsgId = ResolveYouthPackageMsgId(req.Amount);
            if (packageMsgId == 0)
                return;

            string packageDesc = await BuildTemplateAsync(packageMsgId, ct).ConfigureAwait(false);
            if (string.IsNullOrEmpty(packageDesc))
                return;

            if (req.TelNum == req.TelCharger)
            {
                return;
            }

            string date = GetPersianDatePlusDays(data.ExpiryTime);
            string now = DateTime.Now.ToString("HH:mm:ss");

            string text = await BuildTemplateAsync(100012, ct).ConfigureAwait(false);
            text = text
                .Replace("%P_AMOUNT%", req.Amount.ToString("0", CultureInfo.InvariantCulture))
                .Replace("%P_TEL_NUM%", req.TelNum.ToString())
                .Replace("%V_PACKAGE_DESC%", packageDesc)
                .Replace("%V_TIME%", now)
                .Replace("%V_DATE%", date);

            await _smsProvider.SendSms(req.TelCharger.ToString(), text + addendum).ConfigureAwait(false);
        }

        private async Task SendLoyaltyChargeAsync(SendSmsRequest req, string addendum, CancellationToken ct)
        {
            if (req.LoyaltyYear == 0)
                return;

            if (req.TelNum == req.TelCharger)
            {
                string text = await BuildTemplateAsync(100013, ct).ConfigureAwait(false);
                text = text.Replace("%V_YEAR%", req.LoyaltyYear.ToString());
                await _smsProvider.SendSms(req.TelNum.ToString(), text + " " + addendum).ConfigureAwait(false);
            }
            else
            {
                string textToSponsor = await BuildTemplateAsync(100014, ct).ConfigureAwait(false);
                textToSponsor = textToSponsor
                    .Replace("%V_YEAR%", req.LoyaltyYear.ToString())
                    .Replace("%P_FK_TEL_CHARGER%", req.TelCharger.ToString());
                await _smsProvider.SendSms(req.TelNum.ToString(), textToSponsor + " " + addendum).ConfigureAwait(false);

                string textToCharged = await BuildTemplateAsync(100015, ct).ConfigureAwait(false);
                textToCharged = textToCharged
                    .Replace("%P_CARD_CHARGE%", req.CardCharge.ToString("0", CultureInfo.InvariantCulture))
                    .Replace("%P_TEL_NUM%", req.TelNum.ToString());
                await _smsProvider.SendSms(req.TelCharger.ToString(), textToCharged).ConfigureAwait(false);
            }
        }

        private async Task<decimal> ResolveFaceAmountAsync(SendSmsRequest req, CancellationToken ct)
        {
            SelectedBrokerResponseModel? gift = await TryRpcAsync<SelectedBrokerRequestModel, SelectedBrokerResponseModel>(
                "infra.cache.selected-brokers",
                new SelectedBrokerRequestModel { BrokerId = req.BrokerId, OfferId = req.ExtraChargeId },
                ct).ConfigureAwait(false);

            if (gift is null)
                return req.Amount;

            decimal pct = gift.GiftPercent;
            return Math.Round(req.Amount * (100 + pct) / 100);
        }

        private async Task<string> ResolveBrokerSmsAddendumAsync(decimal brokerId, decimal extraChargeId, decimal offerCode, decimal telNum, decimal telCharger, CancellationToken ct)
        {
            BrokerSmsResponseModel? brokerText = await TryRpcAsync<BrokerSmsRequestModel, BrokerSmsResponseModel>(
                "infra.cache.broker-sms-text",
                new BrokerSmsRequestModel { BrokerId = brokerId, RequestType = extraChargeId },
                ct).ConfigureAwait(false);

            if (brokerText is not null)
                return "\n" + brokerText.SmsText;

            if (offerCode == OfferLoyalty && telCharger != telNum)
            {
                BrokerSmsResponseModel? fallback = await TryRpcAsync<BrokerSmsRequestModel, BrokerSmsResponseModel>(
                    "infra.cache.broker-sms-text",
                    new BrokerSmsRequestModel { SmsId = 2 },
                    ct).ConfigureAwait(false);

                if (fallback is not null)
                    return "\n" + fallback.SmsText;
            }

            return string.Empty;
        }

        private async Task<string> BuildTemplateAsync(int messageId, CancellationToken ct)
        {
            MessagesResponseModel? template = await TryRpcAsync<MessagesRequestModel, MessagesResponseModel>(
                "infra.cache.messages",
                new MessagesRequestModel { MessageId = messageId, MethodName = "PINLESS_SMS" },
                ct).ConfigureAwait(false);

            return template?.MessageText ?? string.Empty;
        }

        private static int ResolveYouthPackageMsgId(decimal amount)
            => amount switch
            {
                50000 => 100007,
                100000 => 100008,
                200000 => 100009,
                500000 => 100010,
                1000000 => 100011,
                _ => 0
            };

        private static string GetPersianDate()
        {
            var pc = new PersianCalendar();
            DateTime now = DateTime.Now;
            return $"{pc.GetYear(now):0000}/{pc.GetMonth(now):00}/{pc.GetDayOfMonth(now):00}";
        }

        private static string GetPersianDatePlusDays(int days)
        {
            var pc = new PersianCalendar();
            DateTime future = DateTime.Now.AddDays(days);
            return $"{pc.GetYear(future):0000}/{pc.GetMonth(future):00}/{pc.GetDayOfMonth(future):00}";
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
                    cancellationToken: ct).ConfigureAwait(false);

                return result.Success ? result.Data : null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "SendSms.TryRpc: call to '{Subject}' threw", subject);
                return null;
            }
        }
    }
}
