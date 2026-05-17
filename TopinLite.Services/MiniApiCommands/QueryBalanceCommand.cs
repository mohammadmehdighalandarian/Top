namespace TopinLite.Services.MiniApiCommands
{
    public class QueryBalanceCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public QueryBalanceCommand(QueryBalanceTcpRequest model)
        {
            QueryBalanceTcpRequest = model;
        }

        public QueryBalanceTcpRequest QueryBalanceTcpRequest { get; set; }
    }

    public class QueryBalanceCommandHandler : IRequestHandler<QueryBalanceCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public QueryBalanceCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(QueryBalanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.QueryBalance(new QueryBalanceTcpRequest
                {
                    Mss = request.QueryBalanceTcpRequest.Mss,
                    PrimaryIdentity = request.QueryBalanceTcpRequest.PrimaryIdentity
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