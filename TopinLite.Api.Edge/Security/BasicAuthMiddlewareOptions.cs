namespace TopinLite.Api.Edge.Security
{
    public sealed class BasicAuthMiddlewareOptions
    {
        /// <summary>
        /// Shown in WWW-Authenticate header.
        /// </summary>
        public string Realm { get; set; } = "Protected";

        /// <summary>
        /// When true, returns JSON body for unauthorized responses.
        /// </summary>
        public bool ReturnJsonBody { get; set; } = true;

        /// <summary>
        /// Paths to exclude from auth.
        /// Example: /health, /swagger, /favicon.ico
        /// </summary>
        public List<PathString> ExcludedPaths { get; set; } = new();

        /// <summary>
        /// If true, empty or malformed auth header logs warning.
        /// </summary>
        public bool LogFailures { get; set; } = true;
    }
}