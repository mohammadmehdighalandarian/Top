using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Commons;
using TopinLite.Domain.Messaging;
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
        private readonly IRpcClient _rpcClient;

        public YouthChargeValidationService(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public async Task<ExecResult> ValidateAsync(RequestOrderRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                RpcResult<QueryCustomerByTelResponseModel> response =
                    _rpcClient.RequestAsync<QueryCustomerByTelRequestModel, QueryCustomerByTelResponseModel>(subject: "crm.querycustomerbytel",
                        new QueryCustomerByTelRequestModel
                        {
                            PrimaryIdentity = request.TelGift.ToString(),
                            Mss = $"Topup/Topin{Guid.NewGuid()}"
                        }, cancellationToken: cancellationToken).GetAwaiter().GetResult();
                if (!response.Success || response.Data?.ResponseDesc is null || response.Data.ResponseType != "0")
                    return Fail(ResultCodes.BadInput);
                
                string birthToken = TokenParser.GetToken(response.Data?.ResponseDesc!, 6, ";");
                if (!DateParser.TryParse(birthToken, out DateTime birthDate, DateParser.BirthDateFormats) || birthDate.AddYears(25).Date < DateTime.UtcNow.Date)
                {
                    return Fail(ResultCodes.YouthChargeAgeLimit);
                }
            }
            catch
            {
                return Fail(ResultCodes.BadInput);
            }

            return Success();
        }

        private static ExecResult Fail(ResultCode rc)
            => new()
            {
                ExecStatus = false,
                ResultCode = rc.Code,
                ResultMessage = rc.Message
            };

        private static ExecResult Success()
            => new()
            {
                ExecStatus = true,
                ResultCode = ResultCodes.Success.Code,
                ResultMessage = ResultCodes.Success.Message
            };
    }
}