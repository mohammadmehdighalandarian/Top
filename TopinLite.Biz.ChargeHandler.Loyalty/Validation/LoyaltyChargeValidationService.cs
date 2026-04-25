using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeHandler.Loyalty.Validation
{
    public interface ILoyaltyChargeValidationService
    {
        Task<ExecResult> ValidateAsync(RequestOrderRequestModel envelope, CancellationToken cancellationToken);
    }

    public sealed class LoyaltyChargeValidationService : ILoyaltyChargeValidationService
    {
        private const decimal InvalidLoyaltyCode = -1028;
        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _endpoint;
        private readonly ITopupMessageProvider _messageProvider;

        public LoyaltyChargeValidationService(
            Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint endpoint,
            ITopupMessageProvider messageProvider)
        {
            _endpoint = endpoint;
            _messageProvider = messageProvider;
        }

        public async Task<ExecResult> ValidateAsync(RequestOrderRequestModel request, CancellationToken cancellationToken)
        {
            decimal resultCode;

            try
            {
                string msisdn = MsisdnNormalizer.Normalize(request.TelGift);
                GeneralHuawiResponse response = await _endpoint.QuerySubscriber(new QuerySubscriberTcpRequest
                {
                    PrimaryIdentity = msisdn,
                    Mss = $"Topup/Topin{Guid.NewGuid()}"
                }).ConfigureAwait(false);

                if (!string.Equals(response.ResponseType, "0", StringComparison.Ordinal))
                {
                    resultCode = ResponseCodeParser.ParseDecimalOrFallback(response.ResponseType, InvalidLoyaltyCode);
                }
                else
                {
                    string dateToken = TokenParser.GetToken(response.ResponseDesc, 8, ';');
                    if (!DateParser.TryParse(dateToken, out DateTime activeDate, DateParser.SubscriberDateFormats))
                    {
                        resultCode = InvalidLoyaltyCode;
                    }
                    else
                    {
                        int activeMonths = DiffMonths(DateTime.UtcNow, activeDate);
                        resultCode = activeMonths < 12 ? InvalidLoyaltyCode : 0;
                    }
                }
            }
            catch
            {
                resultCode = InvalidLoyaltyCode;
            }

            string message = await _messageProvider.ResolveAsync(
                resultCode,
                resultCode == 0 ? "Success Execution" : "Loyalty charge is not allowed for this subscriber.",
                cancellationToken).ConfigureAwait(false);

            return new ExecResult
            {
                ExecStatus = resultCode == 0,
                ResultCode = resultCode,
                ResultMessage = message
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
}
