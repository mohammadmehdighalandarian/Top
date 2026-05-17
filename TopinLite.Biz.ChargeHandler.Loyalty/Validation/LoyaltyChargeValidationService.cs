using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Commons;
using TopinLite.Domain.Messaging;
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
        private readonly IRpcClient _rpcClient;

        public LoyaltyChargeValidationService(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public async Task<ExecResult> ValidateAsync(RequestOrderRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                RpcResult<QuerySubscriberResponseModel> response =
                    _rpcClient.RequestAsync<QuerySubscriberRequestModel, QuerySubscriberResponseModel>(subject: "crm.querysubscriber",
                        new QuerySubscriberRequestModel
                        {
                            PrimaryIdentity = request.TelGift.ToString(),
                            Mss = $"Topup/Topin{Guid.NewGuid()}"
                        }, cancellationToken: cancellationToken).GetAwaiter().GetResult();
                if (!response.Success || response.Data?.ResponseDesc is null || response.Data.ResponseType != "0")
                    return Fail(ResultCodes.BadInput);
                
                string dateToken = TokenParser.GetToken(response.Data?.ResponseDesc!, 8, ";");
                if (!DateParser.TryParse(dateToken, out DateTime activeDate, DateParser.SubscriberDateFormats))
                {
                    return Fail(ResultCodes.BadInput);
                }
                
                int activeMonths = DiffMonths(DateTime.UtcNow, activeDate);
                if (activeMonths < 12)
                {
                    return Fail(ResultCodes.LoyaltyChargeRestriction);
                }
            }
            catch
            {
                return Fail(ResultCodes.BadInput);
            }

            return Success();
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