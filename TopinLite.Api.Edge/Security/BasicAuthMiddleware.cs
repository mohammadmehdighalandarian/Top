using Microsoft.Extensions.Options;

using System.Security.Claims;
using System.Text;

namespace TopinLite.Api.Edge.Security
{
    public sealed class BasicAuthMiddleware : IMiddleware
    {
        private readonly IBasicAuthUserValidator _validator;
        private readonly BasicAuthMiddlewareOptions _options;
        private readonly ILogger<BasicAuthMiddleware> _logger;

        public BasicAuthMiddleware(
            IBasicAuthUserValidator validator,
            IOptions<BasicAuthMiddlewareOptions> options,
            ILogger<BasicAuthMiddleware> logger)
        {
            _validator = validator;
            _options = options.Value;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (ShouldSkip(context.Request.Path))
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(BasicAuthConstants.AuthorizationHeaderName, out var authHeaderValues))
            {
                if (_options.LogFailures)
                    _logger.LogWarning("Missing Authorization header for path {Path}", context.Request.Path);

                await WriteUnauthorizedAsync(context, "Missing Authorization header.");
                return;
            }

            var authHeader = authHeaderValues.ToString();

            if (!TryParseBasicCredentials(authHeader, out var username, out var password))
            {
                if (_options.LogFailures)
                    _logger.LogWarning("Invalid Basic Authorization header for path {Path}", context.Request.Path);

                await WriteUnauthorizedAsync(context, "Invalid Basic authentication header.");
                return;
            }

            BasicAuthResult result;
            try
            {
                result = await _validator.ValidateAsync(username, password, context.RequestAborted);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while validating basic auth user {Username}", username);
                await WriteUnauthorizedAsync(context, "Authentication validation failed.");
                return;
            }

            if (!result.Succeeded)
            {
                if (_options.LogFailures)
                    _logger.LogWarning("Basic authentication failed for user {Username}", username);

                await WriteUnauthorizedAsync(context, result.FailureMessage ?? "Invalid username or password.");
                return;
            }

            SetUserPrincipal(context, result, username);

            await next(context);
        }

        private bool ShouldSkip(PathString requestPath)
        {
            if (_options.ExcludedPaths.Count == 0)
                return false;

            foreach (var excludedPath in _options.ExcludedPaths)
            {
                if (requestPath.StartsWithSegments(excludedPath, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static bool TryParseBasicCredentials(string authorizationHeader, out string username, out string password)
        {
            username = string.Empty;
            password = string.Empty;

            if (string.IsNullOrWhiteSpace(authorizationHeader))
                return false;

            var prefix = BasicAuthConstants.BasicScheme + " ";

            if (!authorizationHeader.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return false;

            var encodedCredentials = authorizationHeader[prefix.Length..].Trim();

            if (string.IsNullOrWhiteSpace(encodedCredentials))
                return false;

            byte[] credentialBytes;
            try
            {
                credentialBytes = Convert.FromBase64String(encodedCredentials);
            }
            catch (FormatException)
            {
                return false;
            }

            var decoded = Encoding.UTF8.GetString(credentialBytes);

            var separatorIndex = decoded.IndexOf(':');
            if (separatorIndex <= 0)
                return false;

            username = decoded[..separatorIndex];
            password = decoded[(separatorIndex + 1)..];

            return !string.IsNullOrWhiteSpace(username);
        }

        private static void SetUserPrincipal(HttpContext context, BasicAuthResult result, string fallbackUsername)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.Name, result.Username ?? fallbackUsername),
            new("auth_scheme", "Basic")
        };

            if (result.Claims is not null && result.Claims.Count > 0)
                claims.AddRange(result.Claims);

            var identity = new ClaimsIdentity(claims, authenticationType: "Basic");
            context.User = new ClaimsPrincipal(identity);
        }

        private async Task WriteUnauthorizedAsync(HttpContext context, string message)
        {
            if (context.Response.HasStarted)
                return;

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.WWWAuthenticate = $@"Basic realm=""{_options.Realm}"", charset=""UTF-8""";

            if (_options.ReturnJsonBody)
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync($$"""
            {
              "error": "unauthorized",
              "message": "{{EscapeJson(message)}}"
            }
            """);
            }
        }

        private static string EscapeJson(string value)
        {
            return value
                .Replace("\\", "\\\\", StringComparison.Ordinal)
                .Replace("\"", "\\\"", StringComparison.Ordinal)
                .Replace("\r", "\\r", StringComparison.Ordinal)
                .Replace("\n", "\\n", StringComparison.Ordinal);
        }
    }
}