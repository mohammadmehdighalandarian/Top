namespace TopinLite.Services.MiniApiCommands
{
    public class QuerySubscriberCZ2Command : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public QuerySubscriberCZ2Command(QuerySubscriberCZ2TcpRequest model)
        {
            QuerySubscriberCZ2TcpRequest = model;
        }

        public QuerySubscriberCZ2TcpRequest QuerySubscriberCZ2TcpRequest { get; set; }
    }

    public class QuerySubscriberCZ2CommandHandler : IRequestHandler<QuerySubscriberCZ2Command, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public QuerySubscriberCZ2CommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(QuerySubscriberCZ2Command request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.QuerySubscriberCZ2(new QuerySubscriberCZ2TcpRequest
                {
                    Mss = request.QuerySubscriberCZ2TcpRequest.Mss,
                    PrimaryIdentity = request.QuerySubscriberCZ2TcpRequest.PrimaryIdentity,
                    ProviderId = request.QuerySubscriberCZ2TcpRequest.ProviderId
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