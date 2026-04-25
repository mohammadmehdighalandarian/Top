using TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry;
using TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber;
using TopinLite.Domain.HuawiMicroGateway;

namespace TopinLite.Api.Edge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IRpcClient _rpcClient;
        private readonly IMediator _mediator;

        public MainController(IRpcClient rpcClient, IMediator mediator)
        {
            _rpcClient = rpcClient;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/rest/Token")]
        public async Task<IActionResult> Token()
        {
            return Ok(new { Token = "ksdbfogg897w6f87nq3r4w" });
        }

        [HttpGet]
        [Route("/GetProductTypeList")]
        public async Task<ExecResult<IEnumerable<ProductTypeModel>>> GetProductTypeList()
        {
            return new ExecResult<IEnumerable<ProductTypeModel>>();
        }

        [HttpGet]
        [Route("/GetProductList")]
        public async Task<ExecResult<IEnumerable<ProductModel>>> GetProductList(string ProductId)
        {
            return new ExecResult<IEnumerable<ProductModel>>();
        }

        [HttpPost]
        [Route("/PackagesListQuery")]
        public async Task<IActionResult> PackagesListQuery()
        {
            return Ok();
        }

        [HttpPost]
        [Route("/CalculateDIY")]
        public async Task<ExecResult> CalculateDIY(CalculateDIYModel model)
        {
            return new ExecResult();
        }

        [HttpPost]
        [Route("/RequestOrder")]
        [AllowAnonymous]
        public async Task<ExecResult<RequestOrderResponseModel>> RequestOrder(RequestOrderRequestModel model)
        {
            return await _mediator.Send(new RequestOrderCommand(model));
        }

        [HttpPost]
        [Route("/ConfirmOrder")]
        public async Task<ExecResult<ConfirmOrderResponseModel>> ConfirmOrder(ConfirmOrderRequestModel model)
        {
            return new ExecResult<ConfirmOrderResponseModel>
            {
                ExecStatus = false,
                ResultCode = -9006,
                ResultMessage = "No response from charge handler."
            };
        }

        [HttpPost]
        [Route("/QueryBalance")]
        public async Task<ExecResult> QueryBalance(Domain.HuaweiApiModel.AR.QueryBalanceRequestModel model)
        {
            return new ExecResult();
        }

        [HttpPost]
        [Route("/CheckOfferEligibility")]
        public async Task<ExecResult<GeneralHuawiResponse>> CheckOfferEligibility(CheckOfferEligibilityTcpRequest model, CancellationToken cancellationToken)
        {
            var result = await _rpcClient.RequestAsync<CheckOfferEligibilityTcpRequest, GeneralHuawiResponse>
            (
                subject: "crm.checkoffereligibility",
                request: model,
                options: new RpcCallOptions
                {
                    Timeout = TimeSpan.FromSeconds(3),
                    TraceId = Guid.NewGuid().ToString("N")
                },
                cancellationToken: cancellationToken
            );
            
            if (result.Data == null)
                return new ExecResult<GeneralHuawiResponse>
                {
                    ExecStatus = false,
                    ResultCode = 500,
                    ResultMessage = "No response data returned from CheckOfferEligibility service."
                };

            return new ExecResult<GeneralHuawiResponse>
            {
                ExecStatus = result.Success,
                ResultCode = Convert.ToDecimal(result.Code),
                ResultMessage = result.Data.ResponseDesc
            };
        }

        [HttpPost]
        [Route("/QuerySimType")]
        public async Task<ExecResult<GeneralHuawiResponse>> QuerySimType(QuerySimTypeTcpRequest model, CancellationToken cancellationToken)
        {
            var result = await _rpcClient.RequestAsync<QuerySimTypeTcpRequest, GeneralHuawiResponse>
            (
                subject: "crm.querysimtype",
                request: model,
                options: new RpcCallOptions
                {
                    Timeout = TimeSpan.FromSeconds(3),
                    TraceId = Guid.NewGuid().ToString("N")
                },
                cancellationToken: cancellationToken
            );
            
            if (result.Data == null)
                return new ExecResult<GeneralHuawiResponse>
                {
                    ExecStatus = false,
                    ResultCode = 500,
                    ResultMessage = "No response data returned from QuerySimType service."
                };

            return new ExecResult<GeneralHuawiResponse>
            {
                ExecStatus = result.Success,
                ResultCode = Convert.ToDecimal(result.Code),
                ResultMessage = result.Data.ResponseDesc
            };
        }

        [HttpPost]
        [Route("/ActiveLoan")]
        public async Task<ExecResult> ActiveLoan(ActiveLoanModel model)
        {
            return new ExecResult();
        }

        [HttpPost]
        [Route("/QueryQuata")]
        public async Task<ExecResult> QueryQuata(QueryQuataModel model)
        {
            return new ExecResult();
        }

        [HttpPost]
        [Route("/GetHamrahiPackageInfo")]
        public async Task<GetHamrahiPackageInfoResV2<HamrahiDetails>> GetHamrahiPackageInfo(GetHamrahiPackagesInfoModel model)
        {
            return new GetHamrahiPackageInfoResV2<HamrahiDetails>();
        }

        [HttpPost]
        [Route("/GetLoanEligibleOffers")]
        public async Task<GetLoanEligibleOffersResponseModel> GetLoanEligibleOffers(GetLoanEligibleOffersRequestModel model)
        {
            return new GetLoanEligibleOffersResponseModel();
        }

        [HttpPost]
        [Route("/GetHamrahiPackagesInfoV2")]
        public async Task<GetHamrahiPackageInfoResV2<HamrahiDetails>> GetHamrahiPackageInfoV2(GetHamrahiPackagesInfoModel model)
        {
            return new GetHamrahiPackageInfoResV2<HamrahiDetails>();
        }

        [HttpPost]
        [Route("/GetHamrahiPackagesInfoV1")]
        public async Task<GetHamrahiPackageInfoRes> GetHamrahiPackagesInfoV1(GetHamrahiPackagesInfoV1 model)
        {
            return new GetHamrahiPackageInfoRes();
        }

        [HttpPost]
        [Route("/QuerySubscriber")]
        public async Task<GeneralHuawiResponse<QuerySubscriberResponse>> QuerySubscriber(QuerySubscriberTcpRequest model)
        {
            return new GeneralHuawiResponse<QuerySubscriberResponse>();
        }

        [HttpPost]
        [Route("/IntegrationEnquery")]
        public async Task<GeneralHuawiResponse<IntegrationEnquiryResult>> IntegrationEnquery(IntegrationEnquiryTcpRequest model)
        {
            return new GeneralHuawiResponse<IntegrationEnquiryResult>();
        }

        [HttpPost]
        [Route("/QueryCustomerInfoByTel")]
        public async Task<ExecResult<QueryCustomerInfoByTelResponseModel>> QueryCustomerInfoByTel(QueryCustomerInfoByTelRequestModel model)
        {
            return new ExecResult<QueryCustomerInfoByTelResponseModel>();
        }
    }
}