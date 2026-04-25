using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeSaleHandler.ServiceProviders
{

    public interface IPackageServiceProvider
    {
        Task<ExecResult> Init(RequestOrderRequestModel model);
    }


    public class ChargeServiceProvider: IPackageServiceProvider
    {

        public async Task<ExecResult> Init (RequestOrderRequestModel model)
        {

            return new ExecResult { ExecStatus = true, ResultMessage = "Success" };
        }
    }
}
