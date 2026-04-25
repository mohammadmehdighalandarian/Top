namespace TopinLite.Infrastructure.CacheData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ServiceProviders.IInMemoryDataProvider _MemoryService;
        public AdminController(ServiceProviders.IInMemoryDataProvider memoryService)
        {
            _MemoryService = memoryService;
        }



        [HttpGet("Offers")]
        public async Task<OffersModel> GetOfferById([FromQuery] int Id, CancellationToken cancellationToken)
        {
            OffersModel DataResult = await _MemoryService.GetOfferInfoByOfferId(Id);

            if (DataResult is not null)
            {
                return DataResult;
            }

            return new OffersModel();
        }

        [HttpGet("Brokers")]
        public async Task<BrokersModel> GetBrokersBySapId([FromQuery] int Id, CancellationToken cancellationToken)
        {
            BrokersModel DataResult = await _MemoryService.GetBrokerInfoBySapId(Id);

            if (DataResult is not null)
            {
                return DataResult;
            }

            return new BrokersModel();
        }

        [HttpGet("BrokersAccess")]
        public async Task<BrokersAccessModel> GetBrokersAccessBySapIdAndMethodName([FromQuery] Int64 Id, [FromQuery] string MethodName, CancellationToken cancellationToken)
        {
            BrokersAccessModel DataResult = await _MemoryService.GetBrokersAccessBySapIdAndMethodName(Id, MethodName);

            if (DataResult is not null)
            {
                return DataResult;
            }

            return new BrokersAccessModel();
        }


        [HttpGet("DynamicConditions")]
        public async Task<DynamicConditionModel> GetDynamicConditions([FromQuery] Int64 Id, [FromQuery] string Biztype, [FromQuery] string  KeyStr, CancellationToken cancellationToken)
        {
            DynamicConditionModel DataResult = await _MemoryService.GetDynamicConditionsByParameters(Id,Biztype,KeyStr);

            if (DataResult is not null)
            {
                return DataResult;
            }

            return new DynamicConditionModel();
        }
    }
}
