namespace TopinLite.Services.MiniApiCommands
{
    public class QuerySimTypeCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public QuerySimTypeCommand(QuerySimTypeTcpRequest model)
        {
            QuerySimTypeTcpRequest = model;
        }

        public QuerySimTypeTcpRequest QuerySimTypeTcpRequest { get; set; }
    }

    public class QuerySimTypeCommandHandler : IRequestHandler<QuerySimTypeCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public QuerySimTypeCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(QuerySimTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.QuerySimType(new QuerySimTypeTcpRequest
                {
                    Mss = request.QuerySimTypeTcpRequest.Mss,
                    SubscriberNo = request.QuerySimTypeTcpRequest.SubscriberNo
                });

                return new ExecResult<GeneralHuawiResponse>
                {
                    Data = Result,
                    ExecStatus = true,
                    ResultMessage = Result.ResponseDesc
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception CRM Message:{ex.Message} & Trace:{ex.StackTrace}");

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