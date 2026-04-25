using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeHandler.Direct.Validation
{
    public interface IDirectChargeValidationService
    {
        Task<ExecResult> ValidateAsync(RequestOrderRequestModel request, CancellationToken cancellationToken);
    }

    public sealed class DirectChargeValidationService : IDirectChargeValidationService
    {
        public Task<ExecResult> ValidateAsync(RequestOrderRequestModel request, CancellationToken cancellationToken)
        {
            if (request.payloadId != "1001" && request.payloadId != "1002")
            {
                return Task.FromResult(new ExecResult
                {
                    ExecStatus = false,
                    ResultCode = 7,
                    ResultMessage = "Invalid direct charge payload."
                });
            }

            return Task.FromResult(new ExecResult
            {
                ExecStatus = true,
                ResultCode = 0,
                ResultMessage = "Success Execution"
            });
        }
    }
}
