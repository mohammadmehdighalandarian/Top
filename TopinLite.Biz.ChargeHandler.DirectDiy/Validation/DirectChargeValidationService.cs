using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeHandler.DirectDiy.Validation
{
    public interface IDirectChargeValidationService
    {
        Task<ExecResult> ValidateAsync(RequestOrderRequestModel request, CancellationToken cancellationToken);
    }

    public sealed class DirectChargeValidationService : IDirectChargeValidationService
    {
        public Task<ExecResult> ValidateAsync(RequestOrderRequestModel request, CancellationToken cancellationToken)
        {
            if (request.PayloadId != "1001" && request.PayloadId != "1002")
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
