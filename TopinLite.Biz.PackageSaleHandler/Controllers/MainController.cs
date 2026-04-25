using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TopinLite.Biz.PackageSaleHandler.ServiceProviders;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.PackageSaleHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IPackageServiceProvider _IPackageServiceProvider;

        public MainController(IPackageServiceProvider iPackageServiceProvider)
        {
            _IPackageServiceProvider = iPackageServiceProvider;
        }


        [HttpPost]
        [Route("/Init")]
        public async Task<ExecResult> Init([FromBody] RequestOrderRequestModel model)
        {


            return new ExecResult
            {
                ExecStatus = true
            };
        }
    }
}
