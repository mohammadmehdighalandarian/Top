using System.Text.RegularExpressions;
using BPJ.Common.Security;
using Microsoft.IO;
using TopinLite.Domain.LogModels;
using TopinLite.Infra.MsBroker.RabbitMQ;

namespace TopinLite.Api.Edge.Middleware
{
    public partial class UserActivityLog
    {
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly RequestDelegate _next;
        private readonly ILogger<UserActivityLog> _logger;
        private readonly IRabbitMQBroker _rabbitService;
        
        public UserActivityLog(RequestDelegate next, ILogger<UserActivityLog> logger, IRabbitMQBroker rabbitService)
        {
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _next = next;
            _logger = logger;
            _rabbitService = rabbitService;
        }

        public async Task Invoke(HttpContext context, CancellationToken token)
        {
            try
            {
                List<string> excludedPaths = [];
                List<Regex> excludedPathsRegex = [new Regex(@"\/captcha*")];

                //remove log from qr code generator
                if (excludedPaths.Contains(context.Request.Path.Value, StringComparer.OrdinalIgnoreCase) || excludedPathsRegex.Any(x => x.IsMatch(context.Request.Path.Value.ToLower())))
                {
                    await _next(context);
                    return;
                }
                await ProcessRequest(context, token);
                await ProcessResponse(context, token);
            }
            catch (Exception)
            {
                await _next(context);
                return;
            }
        }

        private async Task ProcessRequest(HttpContext context, CancellationToken token)
        {
            try
            {
                context.Request.EnableBuffering();
                await using MemoryStream requestStream = _recyclableMemoryStreamManager.GetStream();
                await context.Request.Body.CopyToAsync(requestStream, token);
                string body = ReadStreamInChunks(requestStream);

                #region Check if any secure data is in request body so encrypt the value of requet body
                List<string> securePaths = [];
                List<Regex> securePathsRegex = [new Regex(@"\/auth\/login\/*"), new Regex(@"\/user\/changepassword\/*")];
                //hash body value
                if (securePaths.Contains(context.Request.Path.Value, StringComparer.OrdinalIgnoreCase) || securePathsRegex.Any(x => x.IsMatch(context.Request.Path.Value.ToLower())))
                {
                    body = Rijndael.Encrypt(body);
                }
                #endregion
                UserActivityCallLogModel logData = new()
                {
                    TraceIdentifier = context.TraceIdentifier,
                    Body = body,
                    Direction = 1,
                    EventDate = GetUnixTimeNow(),
                    Path = context.Request.Path,
                    Verb = context.Request.Method,
                    Headers = ConvertHttpHeadersToString(context.Request.Headers),
                    ConnectionInfo = $"Remote:{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort} |  Local:{context.Connection.LocalIpAddress}:{context.Connection.LocalPort}",
                    StatusCode = 0
                };
                _ = _rabbitService.UserActivityCallLog(logData, token);

                context.Request.Body.Position = 0;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In ProcessRequest In Logging Task Message:{ExMessage} And StackTrace:{ExStackTrace} .", ex.Message, ex.StackTrace);
            }
        }

        private async Task ProcessResponse(HttpContext context, CancellationToken token)
        {
            try
            {
                Stream originalBodyStream = context.Response.Body;
                await using MemoryStream responseBody = _recyclableMemoryStreamManager.GetStream();
                context.Response.Body = responseBody;
                await _next(context);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string bodyString = await new StreamReader(context.Response.Body).ReadToEndAsync(token);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                UserActivityCallLogModel logData = new()
                {
                    TraceIdentifier = context.TraceIdentifier,
                    Body = bodyString,
                    Direction = 2,
                    EventDate = GetUnixTimeNow(),
                    Path = context.Request.Path,
                    Verb = context.Request.Method,
                    Headers = ConvertHttpHeadersToString(context.Response.Headers),
                    StatusCode = context.Response.StatusCode
                };

                _ = _rabbitService.UserActivityCallLog(logData, token);

                await responseBody.CopyToAsync(originalBodyStream, token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In ProcessResponse In Logging Task Message:{ExMessage} And StackTrace:{ExStackTrace} .", ex.Message, ex.StackTrace);
            }
        }
    }

    public static class UserActivityLogExtensions
    {
        public static IApplicationBuilder AddUserActivityLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserActivityLog>();
        }
    }
}
