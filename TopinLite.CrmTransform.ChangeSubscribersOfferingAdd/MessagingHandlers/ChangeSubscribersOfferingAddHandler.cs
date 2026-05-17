namespace TopinLite.CrmTransform.ChangeSubscribersOfferingAdd.MessagingHandlers
{
    public class ChangeSubscribersOfferingAddHandler : IRpcHandler<ChangeSubscribersOfferingAddRequestModel, ChangeSubscribersOfferingAddResponseModel>
    {
        private readonly IEndpoint _IEndpoint;

        public ChangeSubscribersOfferingAddHandler(IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        public ValueTask<RpcResult<ChangeSubscribersOfferingAddResponseModel>> HandleAsync(RpcContext context, ChangeSubscribersOfferingAddRequestModel request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.HuawiMicroGateway.GeneralHuawiResponse response = _IEndpoint.ChangeSubscribersOfferingAdd(new ChangeSubscribersOfferingAddTcpRequest
                {
                    BrokerId = request.BrokerId,
                    CycleDiscount = request.CycleDiscount,
                    Data = request.Data,
                    Mss = request.Mss,
                    OfferingId = request.OfferingId,
                    PrimaryIdentity = request.PrimaryIdentity,
                    SponserMsisdn = request.SponserMsisdn,
                    SMS = request.SMS,
                    Voice = request.Voice
                }).GetAwaiter().GetResult();

                return ValueTask.FromResult(RpcResult<ChangeSubscribersOfferingAddResponseModel>.Ok(new ChangeSubscribersOfferingAddResponseModel
                {
                    ResponseDesc = response.ResponseDesc,
                    ResponseType = response.ResponseType
                }));
            }
            catch (Exception ex)
            {
                return ValueTask.FromResult(RpcResult<ChangeSubscribersOfferingAddResponseModel>.Fail("Exception", ex.Message));
            }
        }
    }
}