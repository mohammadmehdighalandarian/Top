namespace TopinLite.Services.MiniApiCommands
{
    public class CheckOfferEligibilityCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public CheckOfferEligibilityCommand(CheckOfferEligibilityTcpRequest model)
        {
            CheckOfferEligibilityTcpRequest = model;
        }

        public CheckOfferEligibilityTcpRequest CheckOfferEligibilityTcpRequest { get; set; }
    }

    public class CheckOfferEligibilityCommandHandler : IRequestHandler<CheckOfferEligibilityCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public CheckOfferEligibilityCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(CheckOfferEligibilityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.CheckOfferEligibility(new CheckOfferEligibilityTcpRequest
                {
                    Mss = request.CheckOfferEligibilityTcpRequest.Mss,
                    PrimaryIdentity = request.CheckOfferEligibilityTcpRequest.PrimaryIdentity,
                    OfferId = request.CheckOfferEligibilityTcpRequest.OfferId
                });

                decimal.TryParse(Result.ResponseType, out decimal resultCode);

                return new ExecResult<GeneralHuawiResponse>
                {
                    Data = Result,
                    ExecStatus = true,
                    ResultCode = resultCode,
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