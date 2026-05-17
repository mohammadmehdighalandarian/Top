using TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry;

namespace TopinLite.Services.MiniApiCommands
{
    public class IntegrationEnquiryCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public IntegrationEnquiryCommand(IntegrationEnquiryTcpRequest model)
        {
            IntegrationEnquiryTcpRequest = model;
        }

        public IntegrationEnquiryTcpRequest IntegrationEnquiryTcpRequest { get; set; }
    }

    public class IntegrationEnquiryCommandHandler : IRequestHandler<IntegrationEnquiryCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public IntegrationEnquiryCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(IntegrationEnquiryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.IntegrationEnquiry(new IntegrationEnquiryTcpRequest
                {
                    Mss = request.IntegrationEnquiryTcpRequest.Mss,
                    PrimaryIdentity = request.IntegrationEnquiryTcpRequest.PrimaryIdentity,
                    PrimaryOffers = request.IntegrationEnquiryTcpRequest.PrimaryOffers,
                    PrimaryOfferWhiteLists = request.IntegrationEnquiryTcpRequest.PrimaryOfferWhiteLists
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