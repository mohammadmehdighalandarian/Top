namespace TopinLite.Services.MiniApiCommands
{
    public class RechargeByBrokerCommand : IRequest<ExecResult<GeneralHuawiResponse>>
    {
        public RechargeByBrokerCommand(RechargeByBrokerTcpRequest model)
        {
            RechargeByBrokerTcpRequest = model;
        }

        public RechargeByBrokerTcpRequest RechargeByBrokerTcpRequest { get; set; }
    }

    public class RechargeByBrokerCommandHandler : IRequestHandler<RechargeByBrokerCommand, ExecResult<GeneralHuawiResponse>>
    {
        #region Construction

        private readonly Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint _IEndpoint;

        public RechargeByBrokerCommandHandler(Infra.ApiClient.SOAPApi.HuaweiEndpoint.IEndpoint iEndpoint)
        {
            _IEndpoint = iEndpoint;
        }

        #endregion Construction

        public async Task<ExecResult<GeneralHuawiResponse>> Handle(RechargeByBrokerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralHuawiResponse Result = await _IEndpoint.RechargeByBroker(new RechargeByBrokerTcpRequest
                {
                    Amount = request.RechargeByBrokerTcpRequest.Amount,
                    BrokerId = request.RechargeByBrokerTcpRequest.BrokerId,
                    Mss = request.RechargeByBrokerTcpRequest.Mss,
                    PrimaryIdentity = request.RechargeByBrokerTcpRequest.PrimaryIdentity,
                    RechargeChannelID = request.RechargeByBrokerTcpRequest.RechargeChannelID,
                    TradeType = request.RechargeByBrokerTcpRequest.TradeType,
                    BeId = request.RechargeByBrokerTcpRequest.BeId
                });

                decimal.TryParse(Result.ResponseType, out decimal resultCode);

                return new ExecResult<GeneralHuawiResponse>
                {
                    Data = Result,
                    ResultCode = resultCode,
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