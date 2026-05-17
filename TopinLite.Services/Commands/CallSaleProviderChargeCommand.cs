

namespace TopinLite.Services.Commands
{
    public class CallSaleProviderChargeCommand : IRequest<ExecResult<ExecResult>>
    {
        public CallSaleProviderChargeCommand(CallSaleProviderRequestModel model)
        {
            ChargeCallSaleProviderModel = model;
        }

        public CallSaleProviderRequestModel ChargeCallSaleProviderModel { get; set; }
    }

    public class CallSaleProviderChargeCommandHandler : IRequestHandler<CallSaleProviderChargeCommand, ExecResult<ExecResult>>
    {
        #region Construction

        public CallSaleProviderChargeCommandHandler()
        {
        }

        #endregion Construction

        public async Task<ExecResult<ExecResult>> Handle(CallSaleProviderChargeCommand request, CancellationToken cancellationToken)
        {
            return new ExecResult<ExecResult>();
        }
    }
}