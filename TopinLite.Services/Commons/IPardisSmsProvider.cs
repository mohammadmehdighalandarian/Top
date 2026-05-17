using TopinLite.Domain.Pardis;

namespace TopinLite.Services.Commons
{
    public interface IPardisSmsProvider
    {
        //Task<HttpContainer<PardisAuthResponseModel>> GetToken(string username, string password);
        Task<string> SendSms(string msisdn, string message);
    }
}
