using System.Security.Claims;

namespace TopinLite.Api.Edge.Security
{
    public sealed class BasicAuthResult
    {
        public bool Succeeded { get; init; }

        public string? FailureMessage { get; init; }

        /// <summary>
        /// Optional username confirmed by validator.
        /// </summary>
        public string? Username { get; init; }

        /// <summary>
        /// Optional claims if you want them later.
        /// </summary>
        public IReadOnlyCollection<Claim>? Claims { get; init; }

        public static BasicAuthResult Success(string username, IReadOnlyCollection<Claim>? claims = null)
            => new()
            {
                Succeeded = true,
                Username = username,
                Claims = claims
            };

        public static BasicAuthResult Fail(string message)
            => new()
            {
                Succeeded = false,
                FailureMessage = message
            };
    }
}