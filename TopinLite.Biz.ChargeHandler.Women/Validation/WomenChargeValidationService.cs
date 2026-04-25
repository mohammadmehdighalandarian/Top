using System.Globalization;

using TopinLite.Domain.HuawiMicroGateway;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeHandler.Women.Validation
{
    public interface IWomenChargeValidationService
    {
        Task<ExecResult> ValidateAsync(RequestOrderRequestModel envelope, CancellationToken cancellationToken);
    }

    public sealed class WomenChargeValidationService : IWomenChargeValidationService
    {
        private const decimal InvalidWomenCode = -1027;
        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _endpoint;
        private readonly ITopupMessageProvider _messageProvider;

        public WomenChargeValidationService(
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
                    resultCode = ResponseCodeParser.ParseDecimalOrFallback(response.ResponseType, InvalidWomenCode);
                }
                else
                {
                    string sexToken = TokenParser.GetToken(response.ResponseDesc, 5, ';');
                    if (!int.TryParse(sexToken, NumberStyles.Integer, CultureInfo.InvariantCulture, out int sex))
                    {
                        resultCode = InvalidWomenCode;
                    }
                    else
                    {
                        resultCode = sex != 0 ? InvalidWomenCode : 0;
                    }
                }
            }
            catch
            {
                resultCode = InvalidWomenCode;
            }

            string message = await _messageProvider.ResolveAsync(
                resultCode,
                resultCode == 0 ? "Success Execution" : "Women charge is not allowed for this subscriber.",
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
