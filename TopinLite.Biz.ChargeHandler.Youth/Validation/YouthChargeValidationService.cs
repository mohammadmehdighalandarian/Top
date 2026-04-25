using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeHandler.Youth.Validation
{
    public interface IYouthChargeValidationService
    {
        Task<ExecResult> ValidateAsync(RequestOrderRequestModel envelope, CancellationToken cancellationToken);
    }

    public sealed class YouthChargeValidationService : IYouthChargeValidationService
    {
        private const decimal InvalidYouthCode = -1023;
        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _endpoint;
        private readonly ITopupMessageProvider _messageProvider;

        public YouthChargeValidationService(
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
                GeneralHuawiResponse response = await _endpoint.QueryCustomerInfoByTel(new QueryCustomerInfoByTelTcpRequest
                {
                    PrimaryIdentity = msisdn,
                    Mss = $"Topup/Topin{Guid.NewGuid()}"
                }).ConfigureAwait(false);

                if (!string.Equals(response.ResponseType, "0", StringComparison.Ordinal))
                {
                    resultCode = ResponseCodeParser.ParseDecimalOrFallback(response.ResponseType, InvalidYouthCode);
                }
                else
                {
                    string birthToken = TokenParser.GetToken(response.ResponseDesc, 6, ';');
                    if (!DateParser.TryParse(birthToken, out DateTime birthDate, DateParser.BirthDateFormats))
                    {
                        resultCode = InvalidYouthCode;
                    }
                    else if (birthDate.AddYears(25).Date < DateTime.UtcNow.Date)
                    {
                        resultCode = InvalidYouthCode;
                    }
                    else
                    {
                        resultCode = 0;
                    }
                }
            }
            catch
            {
                resultCode = InvalidYouthCode;
            }

            string message = await _messageProvider.ResolveAsync(
                resultCode,
                resultCode == 0 ? "Success Execution" : "Youth charge is not allowed for this subscriber.",
                cancellationToken).ConfigureAwait(false);

            return new ExecResult
            {
                ExecStatus = resultCode == 0,
                ResultCode = resultCode,
                ResultMessage = message
            };
        }
    }
}
