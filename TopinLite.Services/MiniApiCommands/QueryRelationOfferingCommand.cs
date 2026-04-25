namespace TopinLite.Services.MiniApiCommands
{
    public class QueryRelationOfferingCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public QueryRelationOfferingCommand(QueryRelationOfferingTcpRequest model)
        {
            QueryRelationOfferingTcpRequest = model;
        }

        public QueryRelationOfferingTcpRequest QueryRelationOfferingTcpRequest { get; set; }
    }

    public class QueryRelationOfferingCommandHandler : IRequestHandler<QueryRelationOfferingCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public QueryRelationOfferingCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(QueryRelationOfferingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.QueryRelationOffering(new QueryRelationOfferingTcpRequest
                {
                    MessageSeq = request.QueryRelationOfferingTcpRequest.MessageSeq,
                    MSISDN = request.QueryRelationOfferingTcpRequest.MSISDN,
                    RelationType = request.QueryRelationOfferingTcpRequest.RelationType,
                    OfferId = request.QueryRelationOfferingTcpRequest.OfferId
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