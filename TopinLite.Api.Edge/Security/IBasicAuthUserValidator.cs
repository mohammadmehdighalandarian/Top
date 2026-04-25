namespace TopinLite.Api.Edge.Security
{
    public interface IBasicAuthUserValidator
    {
        Task<BasicAuthResult> ValidateAsync(
            string username,
            string password,
            CancellationToken cancellationToken = default);
    }
}