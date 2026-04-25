using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using NatsRpcFoundation.Services;



using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeSaleHandler.MessagingHandlers
{



    public sealed class RequestOrderPackageHandler : IRpcHandler<RequestOrderRequestMessageModel, RequestOrderResponseMessageModel>
    {
        private readonly ServiceProviders.IPackageServiceProvider _provider;
        private readonly IRpcClient _rpcClient;

        public RequestOrderPackageHandler(ServiceProviders.IPackageServiceProvider provider, IRpcClient rpcClient)
        {
            _provider = provider;
            _rpcClient = rpcClient;
        }

        public ValueTask<RpcResult<RequestOrderResponseMessageModel>> HandleAsync(
            RpcContext context,
            RequestOrderRequestMessageModel request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.payloadId))
            {
                return ValueTask.FromResult(
                    RpcResult<RequestOrderResponseMessageModel>.Fail("VALIDATION_ERROR", "payloadId is required."));
            }
            ///msisdn , msisdn_target , amount , offerid , brokerid , channelid , additionaldata

            if (string.IsNullOrWhiteSpace(request.TelNum.ToString()))
            {
                return ValueTask.FromResult(
                    RpcResult<RequestOrderResponseMessageModel>.Fail("VALIDATION_ERROR", "TelNum is required."));

            }

            RpcResult<DynamicConditionsResponseModel> resultDynamic1 =
               _rpcClient.RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(subject: "biz.packagesale", new DynamicConditionsRequestModel
               {
                   Biztype = "999",
                   DynamicConditionId = 1,
                   KeyStr = "PKG_PINLESS_CHARGE.CALL_SALE_PROVIDER_STOPED"
               }).GetAwaiter().GetResult();


            RpcResult<DynamicConditionsResponseModel> resultDynamic2 =
            _rpcClient.RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(subject: "biz.packagesale", new DynamicConditionsRequestModel
            {
                Biztype = "997",
                DynamicConditionId = 1,
                KeyStr = "PKG_PINLESS_CHARGE.CALL_SALE_PROVIDER_BROKERS"
            }).GetAwaiter().GetResult();

            if (resultDynamic1.Data.ValueStr !="1" && resultDynamic2.Data.ValueStr == "SAPID" ) 
            {
            
            }





            ExecResult ServiceResult = _provider.Init(request).GetAwaiter().GetResult();

            RequestOrderResponseMessageModel response = new RequestOrderResponseMessageModel
            {
                OrderId = ServiceResult.ResultCode
            };

            return ValueTask.FromResult(RpcResult<RequestOrderResponseMessageModel>.Ok(response));
        }
    }
}
