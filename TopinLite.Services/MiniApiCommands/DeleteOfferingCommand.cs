namespace TopinLite.Services.MiniApiCommands
{
    public class DeleteOfferingCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public DeleteOfferingCommand(DeleteOfferingTcpRequest model)
        {
            DeleteOfferingTcpRequest = model;
        }

        public DeleteOfferingTcpRequest DeleteOfferingTcpRequest { get; set; }
    }

    public class DeleteOfferingCommandHandler : IRequestHandler<DeleteOfferingCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public DeleteOfferingCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(DeleteOfferingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.DeleteOffering(new DeleteOfferingTcpRequest
                {
                    Mss = request.DeleteOfferingTcpRequest.Mss,
                    PrimaryIdentity = request.DeleteOfferingTcpRequest.PrimaryIdentity,
                    OfferingId = request.DeleteOfferingTcpRequest.OfferingId
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