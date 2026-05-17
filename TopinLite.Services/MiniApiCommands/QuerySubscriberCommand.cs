namespace TopinLite.Services.MiniApiCommands
{
    public class QuerySubscriberCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public QuerySubscriberCommand(QuerySubscriberTcpRequest model)
        {
            QuerySubscriberTcpRequest = model;
        }

        public QuerySubscriberTcpRequest QuerySubscriberTcpRequest { get; set; }
    }

    public class QuerySubscriberCommandHandler : IRequestHandler<QuerySubscriberCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public QuerySubscriberCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(QuerySubscriberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.QuerySubscriber(new QuerySubscriberTcpRequest
                {
                    Mss = request.QuerySubscriberTcpRequest.Mss,
                    PrimaryIdentity = request.QuerySubscriberTcpRequest.PrimaryIdentity
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