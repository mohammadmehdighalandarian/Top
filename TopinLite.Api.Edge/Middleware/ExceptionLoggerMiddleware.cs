using System.Net;
using TopinLite.Domain.LogModels;
using TopinLite.Infra.MsBroker.RabbitMQ;

namespace TopinLite.Api.Edge.Middleware
{
    public class ExceptionLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRabbitMQBroker _rabbitService;
        public ExceptionLoggerMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, IRabbitMQBroker rabbitService)
        {
            _next = next;
            _rabbitService = rabbitService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ErrorLogModel logData = new(ex, context);
                await _rabbitService.ErrorCallLog(logData,  CancellationToken.None);
                var result = new
                {
                    IsSuccess = false,
                    ResponseDesc = "خطای سیستمی ",
                    ResponseType = (int)HttpStatusCode.InternalServerError
                };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
