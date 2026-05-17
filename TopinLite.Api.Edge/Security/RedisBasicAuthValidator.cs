namespace TopinLite.Api.Edge.Security
{
    public class RedisBasicAuthValidator : IBasicAuthUserValidator
    {
        private readonly IRpcClient _rpcClient;

        public RedisBasicAuthValidator(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public async Task<BasicAuthResult> ValidateAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            RpcResult<BrokersResponseModel> result =
                await _rpcClient.RequestAsync<BrokersRequestModel, BrokersResponseModel>(subject: "infra.cache.brokers",
                                                                                         request: new BrokersRequestModel { SapId = int.Parse(username) },
                                                                                         options: new RpcCallOptions
                                                                                         {
                                                                                             Timeout = TimeSpan.FromSeconds(3),
                                                                                             TraceId = Guid.NewGuid().ToString("N")
                                                                                         },
                                                                                         cancellationToken: cancellationToken);

            // TODO:
            // 1. read user from database by username
            // 2. check if user exists
            // 3. check if user is active
            // 4. verify password hash
            // 5. optionally load roles/claims

            var userExists = false; // replace with your DB logic

            if (!userExists)
                return BasicAuthResult.Fail("Invalid username or password.");

            // Example only:
            var claims = new List<Claim>
        {
            new(ClaimTypes.Role, "ApiUser")
        };

            return BasicAuthResult.Success(username, claims);
        }
    }
}