using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;

using TopinLite.Domain.Messaging;

namespace TopinLite.Services.Commands
{
    public class RequestOrderCommand : IRequest<ExecResult<RequestOrderResponseModel>>
    {
        public RequestOrderRequestModel RequestOrderModel { get; set; }

        public RequestOrderCommand(RequestOrderRequestModel model)
        {
            RequestOrderModel = model;
        }
    }

    public class RequestOrderCommandHandler : IRequestHandler<RequestOrderCommand, ExecResult<RequestOrderResponseModel>>
    {
        private const string ChargeRequestSubject = "biz.chargesale.requestorder";
        private const string PackageRequestSubject = "biz.packagesale.requestorder";

        private readonly IRpcClient _rpcClient;
        public RequestOrderCommandHandler(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public async Task<ExecResult<RequestOrderResponseModel>> Handle(
            RequestOrderCommand request,
            CancellationToken cancellationToken)
        {
            // ProductId == "1" -> Charge flow (Direct/DIY/Women/Youth/Loyalty),
            // ProductId == "2" -> Package flow (existing).
            if (request.RequestOrderModel.ProductId == "1")
            {
                return await DispatchChargeAsync(request.RequestOrderModel, cancellationToken)
                    .ConfigureAwait(false);
            }

            if (request.RequestOrderModel.ProductId == "2")
            {
                return await DispatchPackageAsync(request.RequestOrderModel, cancellationToken)
                    .ConfigureAwait(false);
            }

            return new ExecResult<RequestOrderResponseModel>
            {
                ExecStatus = false,
                ResultCode = 7,
                ResultMessage = $"Unknown ProductId '{request.RequestOrderModel.ProductId}'.",
                Data = new RequestOrderResponseModel()
            };
        }

        private async Task<ExecResult<RequestOrderResponseModel>> DispatchChargeAsync(
            RequestOrderRequestModel model,
            CancellationToken cancellationToken)
        {
            var chargeRequest = new ChargeRequestOrderRequest
            {
                TelNum = model.TelNum,
                TelGift = model.TelGift,
                Amount = model.Amount,
                ProductId = model.ProductId,
                PayloadId = model.PayloadId,
                ChannelId = model.ChannelId,
                Sms = model.Sms,
                Voice = model.Voice,
                Gprs = model.Gprs,
                AdditionalData = model.AdditionalData,
                BrokerId = model.BrokerId,
                CustomerId = model.CustomerId,
                VendorId = model.VendorId
            };

            RpcResult<ChargeRequestOrderResponse> result = await _rpcClient
                .RequestAsync<ChargeRequestOrderRequest, ChargeRequestOrderResponse>(
                    subject: ChargeRequestSubject,
                    request: chargeRequest,
                    options: new RpcCallOptions
                    {
                        Timeout = TimeSpan.FromSeconds(15),
                        TraceId = Guid.NewGuid().ToString("N")
                    },
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!result.Success)
            {
                // Map orchestrator failure to ExecResult shape.
                decimal code = decimal.TryParse(result.Code, out decimal parsed) ? parsed : -1;
                return new ExecResult<RequestOrderResponseModel>
                {
                    ExecStatus = false,
                    ResultCode = code,
                    ResultMessage = result.Message ?? "Charge request failed.",
                    Data = new RequestOrderResponseModel()
                };
            }

            return new ExecResult<RequestOrderResponseModel>
            {
                ExecStatus = true,
                ResultCode = 0,
                ResultMessage = "Success",
                Data = new RequestOrderResponseModel { OrderId = result.Data!.OrderId }
            };
        }

        private async Task<ExecResult<RequestOrderResponseModel>> DispatchPackageAsync(
            RequestOrderRequestModel model,
            CancellationToken cancellationToken)
        {
            RpcResult<RequestOrderResponseMessageModel> result = await _rpcClient
                .RequestAsync<RequestOrderRequestMessageModel, RequestOrderResponseMessageModel>(
                    subject: PackageRequestSubject,
                    request: new RequestOrderRequestMessageModel
                    {
                        AdditionalData = model.AdditionalData,
                        Amount = model.Amount,
                        BrokerId = model.BrokerId,
                        ChannelId = model.ChannelId,
                        CustomerId = model.CustomerId,
                        Gprs = model.Gprs,
                        PayloadId = model.PayloadId
                    },
                    options: new RpcCallOptions
                    {
                        Timeout = TimeSpan.FromSeconds(3),
                        TraceId = Guid.NewGuid().ToString("N")
                    },
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return new ExecResult<RequestOrderResponseModel>
            {
                ExecStatus = result.Success,
                Data = new RequestOrderResponseModel { OrderId = result.Data?.OrderId ?? 0 }
            };
        }
    }
}