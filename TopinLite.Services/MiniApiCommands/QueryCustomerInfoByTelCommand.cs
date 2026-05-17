namespace TopinLite.Services.MiniApiCommands
{
    public class QueryCustomerInfoByTelCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public QueryCustomerInfoByTelCommand(QueryCustomerInfoByTelTcpRequest model)
        {
            QueryCustomerInfoByTelTcpRequest = model;
        }

        public QueryCustomerInfoByTelTcpRequest QueryCustomerInfoByTelTcpRequest { get; set; }
    }

    public class QueryCustomerInfoByTelCommandHandler : IRequestHandler<QueryCustomerInfoByTelCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public QueryCustomerInfoByTelCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(QueryCustomerInfoByTelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.QueryCustomerInfoByTel(new QueryCustomerInfoByTelTcpRequest
                {
                    Mss = request.QueryCustomerInfoByTelTcpRequest.Mss,
                    PrimaryIdentity = request.QueryCustomerInfoByTelTcpRequest.PrimaryIdentity
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