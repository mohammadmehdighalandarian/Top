using TopinLite.Services.Commons;
using TopinLite.Services.MiniApiCommands;
using TopinLite.Services.Orders;


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
        private readonly IRpcClient _rpcClient;
        public RequestOrderCommandHandler(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

       

        public async Task<ExecResult<RequestOrderResponseModel>> Handle(RequestOrderCommand request, CancellationToken cancellationToken)
        {

            if (request.RequestOrderModel.ProductId=="1")
            {
                ///charge
            }



            if (request.RequestOrderModel.ProductId == "2")
            {
                RpcResult<RequestOrderResponseMessageModel> result =
                await _rpcClient.RequestAsync<RequestOrderRequestMessageModel, RequestOrderResponseMessageModel>(subject: "biz.packagesale",
                request: new RequestOrderRequestMessageModel
                {
                    AdditionalData = request.RequestOrderModel.AdditionalData,
                    Amount = request.RequestOrderModel.Amount,
                    BrokerId = request.RequestOrderModel.BrokerId,
                    ChannelId = request.RequestOrderModel.ChannelId,
                    CustomerId = request.RequestOrderModel.CustomerId,
                    Gprs = request.RequestOrderModel.Gprs,
                    payloadId = request.RequestOrderModel.payloadId,
                });

                return new ExecResult<RequestOrderResponseModel>
                {
                    Data = new RequestOrderResponseModel { OrderId = result.Data.OrderId }
                };
            }





            return new ExecResult<RequestOrderResponseModel>
            {
                Data = new RequestOrderResponseModel()
            };
        }
    }
}