namespace TopinLite.Services.MiniApiCommands
{
    public class ChangeSubscribersOfferingAddCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public ChangeSubscribersOfferingAddCommand(ChangeSubscribersOfferingAddTcpRequest model)
        {
            ChangeSubscribersOfferingAddTcpRequest = model;
        }

        public ChangeSubscribersOfferingAddTcpRequest ChangeSubscribersOfferingAddTcpRequest { get; set; }
    }

    public class ChangeSubscribersOfferingAddCommandHandler : IRequestHandler<ChangeSubscribersOfferingAddCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public ChangeSubscribersOfferingAddCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(ChangeSubscribersOfferingAddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.ChangeSubscribersOfferingAdd(new ChangeSubscribersOfferingAddTcpRequest
                {
                    BrokerId = request.ChangeSubscribersOfferingAddTcpRequest.BrokerId,
                    CycleDiscount = request.ChangeSubscribersOfferingAddTcpRequest.CycleDiscount,
                    Data = request.ChangeSubscribersOfferingAddTcpRequest.Data,
                    Mss = request.ChangeSubscribersOfferingAddTcpRequest.Mss,
                    OfferingId = request.ChangeSubscribersOfferingAddTcpRequest.OfferingId,
                    PrimaryIdentity = request.ChangeSubscribersOfferingAddTcpRequest.PrimaryIdentity,
                    SponserMsisdn = request.ChangeSubscribersOfferingAddTcpRequest.SponserMsisdn,
                    SMS = request.ChangeSubscribersOfferingAddTcpRequest.SMS,
                    Voice = request.ChangeSubscribersOfferingAddTcpRequest.Voice
                });

                return new ExecResult<GeneralHuawiResponse>
                {
                    Data = Result,
                    ExecStatus = true,
                    ResultMessage = Result.ResponseDesc
                };
            }
            catch (Exception)
            {
                return new ExecResult<GeneralHuawiResponse>
                {
                    ExecStatus = false,
                    ResultCode = 500,
                    ResultMessage = "An error occurred while processing the request."
                };
            }
        }
    }
}