using System.Globalization;
using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Domain.Messaging;

namespace TopinLite.Services.Commands
{
    public class ConfirmOrderCommand : IRequest<ExecResult<ConfirmOrderResponseModel>>
    {
        public ConfirmOrderRequestModel ConfirmOrderModel { get; set; }

        public ConfirmOrderCommand(ConfirmOrderRequestModel model)
        {
            ConfirmOrderModel = model;
        }
    }

    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, ExecResult<ConfirmOrderResponseModel>>
    {
        private const string Subject = "biz.chargesale.confirmorder";

        private readonly IRpcClient _rpcClient;

        public ConfirmOrderCommandHandler(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public async Task<ExecResult<ConfirmOrderResponseModel>> Handle(
            ConfirmOrderCommand request,
            CancellationToken cancellationToken)
        {
            ConfirmOrderRequestModel? input = request.ConfirmOrderModel;
            if (input is null || input.OrderId <= 0)
            {
                return Fail(-9006, "Order context not found.");
            }

            RpcResult<ChargeConfirmOrderResponse> result = await _rpcClient
                .RequestAsync<ChargeConfirmOrderRequest, ChargeConfirmOrderResponse>(
                    subject: Subject,
                    request: new ChargeConfirmOrderRequest
                    {
                        OrderId = input.OrderId,
                        BankCode = input.BankCode,
                        CardNo = input.CardNo,
                        CardType = input.CardType,
                        RRN = input.RRN
                    },
                    options: new RpcCallOptions
                    {
                        Timeout = TimeSpan.FromSeconds(15),
                        TraceId = Guid.NewGuid().ToString("N")
                    },
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!result.Success || result.Data is null)
            {
                decimal code = decimal.TryParse(result.Code, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed)
                    ? parsed
                    : -9006;

                return new ExecResult<ConfirmOrderResponseModel>
                {
                    ExecStatus = false,
                    ResultCode = code,
                    ResultMessage = result.Message ?? "Confirm order failed.",
                    Data = new ConfirmOrderResponseModel()
                };
            }

            return new ExecResult<ConfirmOrderResponseModel>
            {
                ExecStatus = true,
                ResultCode = 0,
                ResultMessage = "Success",
                Data = result.Data
            };
        }

        private static ExecResult<ConfirmOrderResponseModel> Fail(decimal code, string message) =>
            new()
            {
                ExecStatus = false,
                ResultCode = code,
                ResultMessage = message,
                Data = new ConfirmOrderResponseModel()
            };
    }
}