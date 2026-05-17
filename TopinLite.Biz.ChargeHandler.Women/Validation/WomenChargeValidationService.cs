using System.Globalization;
using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Commons;
using TopinLite.Domain.Messaging;
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
        private readonly IRpcClient _rpcClient;

        public WomenChargeValidationService(IRpcClient rpcClient)
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

                string sexToken = TokenParser.GetToken(response.Data?.ResponseDesc!, 5, ";");
                if (!int.TryParse(sexToken, NumberStyles.Integer, CultureInfo.InvariantCulture, out int sex))
                {
                    return Fail(ResultCodes.BadInput);
                }
                if (sex != 0)
                {
                    return Fail(ResultCodes.WomanChargeRestriction);
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