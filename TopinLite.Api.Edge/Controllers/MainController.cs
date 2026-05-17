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
            return await _mediator.Send(new ConfirmOrderCommand(model));
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
    }
}