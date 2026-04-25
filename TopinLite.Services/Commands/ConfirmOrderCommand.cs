using System.Globalization;

using TopinLite.Services.Orders;

namespace TopinLite.Services.Commands
{
    public class ConfirmOrderCommand : IRequest<ExecResult<ConfirmOrderResponseModel>>
    {
        public ConfirmOrderEnvelope ConfirmOrderModel { get; set; }

        public ConfirmOrderCommand(ConfirmOrderEnvelope model)
        {
            ConfirmOrderModel = model;
        }
    }

    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, ExecResult<ConfirmOrderResponseModel>>
    {
        private readonly IPendingOrderStore _pendingOrderStore;

        public ConfirmOrderCommandHandler(IPendingOrderStore pendingOrderStore)
        {
            _pendingOrderStore = pendingOrderStore;
        }

        public Task<ExecResult<ConfirmOrderResponseModel>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            ConfirmOrderRequestModel? confirm = request.ConfirmOrderModel?.Request;
            if (confirm is null)
            {
                return Task.FromResult(BuildError(19, "Å«—«„ — Ê—ÊœÌ «‘ »«Â «” ."));
            }

            if (!_pendingOrderStore.TryGet(confirm.OrderId, out PendingOrderContext context))
            {
                return Task.FromResult(BuildError(-9006, "Order context not found."));
            }

            decimal offerCode = ParseDecimal(context.PayloadId, 0);
            if (offerCode <= 0)
            {
                return Task.FromResult(BuildError(7, "Invalid payload id."));
            }

            return Task.FromResult(new ExecResult<ConfirmOrderResponseModel>
            {
                ExecStatus = true,
                ResultCode = 0,
                ResultMessage = "⁄„·Ì«  »« „Ê›ﬁÌ  «‰Ã«„ ‘œ.",
                Data = new ConfirmOrderResponseModel
                {
                    OfferCode = (int)offerCode
                }
            });
        }

        private static ExecResult<ConfirmOrderResponseModel> BuildError(decimal code, string message)
        {
            return new ExecResult<ConfirmOrderResponseModel>
            {
                ExecStatus = false,
                ResultCode = code,
                ResultMessage = message,
                Data = new ConfirmOrderResponseModel()
            };
        }

        private static decimal ParseDecimal(string? value, decimal fallback)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed)
                ? parsed
                : fallback;
        }
    }
}