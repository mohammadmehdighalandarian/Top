using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.PackageSaleHandler.ServiceProviders
{

    public interface IPackageServiceProvider
    {
        Task<ExecResult> InitPackage(RequestOrderRequestModel model);
    }


    public class PackageServiceProvider: IPackageServiceProvider
    {

        public async Task<ExecResult> InitPackage (RequestOrderRequestModel model)
        {

            return new ExecResult { ExecStatus = true, ResultMessage = "Success" };
        }
    }
}
