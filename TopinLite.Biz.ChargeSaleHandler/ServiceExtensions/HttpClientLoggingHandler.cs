namespace TopinLite.Biz.ChargeSaleHandler.ServiceExtensions
{
    public class CustomLoggingFilter// : Microsoft.Extensions.Http.IHttpMessageHandlerBuilderFilter
    {
        //private readonly ILoggerFactory _loggerFactory;
        //private readonly Infra.MsBroker.RabbitMQ.IRabbitMQBroker  _IRabbitMQBroker;

        //public CustomLoggingFilter(ILoggerFactory loggerFactory, Infra.MsBroker.RabbitMQ.IRabbitMQBroker rabbitLogger)
        //{
        //    _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        //    _IRabbitMQBroker = rabbitLogger;
        //}

        //public Action<Microsoft.Extensions.Http.HttpMessageHandlerBuilder> Configure(Action<Microsoft.Extensions.Http.HttpMessageHandlerBuilder> next)
        //{
        //    if (next == null)
        //    {
        //        throw new ArgumentNullException(nameof(next));
        //    }

        //    return (builder) =>
        //    {
        //        // Run other configuration first, we want to decorate.
        //        next(builder);

        //        ILogger outerLogger =
        //            _loggerFactory.CreateLogger($"System.Net.Http.HttpClient.{builder.Name}.LogicalHandler");

        //        builder.AdditionalHandlers.Insert(0, new CustomLoggingScopeHttpMessageHandler(outerLogger, _IRabbitMQBroker));
        //    };
        //}
    }

    //public class CustomLoggingScopeHttpMessageHandler : DelegatingHandler
    //{
    //    private readonly ILogger _logger;
    //    //private readonly Infra.MsBroker.RabbitMQ.IRabbitMQBroker _IRabbitMQBroker;

    //    public CustomLoggingScopeHttpMessageHandler(ILogger logger, Infra.MsBroker.RabbitMQ.IRabbitMQBroker rabbitLogger)
    //    {
    //        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    //        _IRabbitMQBroker = rabbitLogger;
    //    }

    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
    //        CancellationToken cancellationToken)
    //    {
    //        if (Domain.CrmTransformServiceConfig.EnableRabbitHttpClientLogger != "1")
    //        {
    //            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
    //            return response;
    //        }

    //        if (request == null)
    //        {
    //            throw new ArgumentNullException(nameof(request));
    //        }

    //        using (Log.BeginRequestPipelineScope(_logger, request))
    //        {
    //            Log.RequestPipelineStart(_logger, request);
    //            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
    //            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
    //            long Elapsed = sw.ElapsedMilliseconds;
    //            Log.RequestPipelineEnd(_logger, response);

    //            Domain.Commons.PartyCallLoggingModel model = new Domain.Commons.PartyCallLoggingModel
    //            {
    //                RequestString = await request.Content.ReadAsStringAsync(),
    //                ResponseString = await response.Content.ReadAsStringAsync(),
    //                ElapsedMS = Elapsed,
    //                RequestURL = request.RequestUri.ToString(),
    //                ResponseCode = (int)response.StatusCode,
    //                From = "ChargeHandler"
    //            };

    //            bool logresult = _IRabbitMQBroker.PartyLogger(model);

    //            return response;
    //        }
    //    }

    //    private static class Log
    //    {
    //        private static class EventIds
    //        {
    //            public static readonly EventId PipelineStart = new EventId(100, "RequestPipelineStart");
    //            public static readonly EventId PipelineEnd = new EventId(101, "RequestPipelineEnd");
    //        }

    //        private static readonly Func<ILogger, HttpMethod, Uri, string, IDisposable> _beginRequestPipelineScope =
    //            LoggerMessage.DefineScope<HttpMethod, Uri, string>(
    //                "HTTP {HttpMethod} {Uri} {CorrelationId}");

    //        private static readonly Action<ILogger, HttpMethod, Uri, string, Exception> _requestPipelineStart =
    //            LoggerMessage.Define<HttpMethod, Uri, string>(
    //                LogLevel.Information,
    //                EventIds.PipelineStart,
    //                "Start processing HTTP request {HttpMethod} {Uri} [Correlation: {CorrelationId}]");

    //        private static readonly Action<ILogger, System.Net.HttpStatusCode, Exception> _requestPipelineEnd =
    //            LoggerMessage.Define<System.Net.HttpStatusCode>(
    //                LogLevel.Information,
    //                EventIds.PipelineEnd,
    //                "End processing HTTP request - {StatusCode}");

    //        public static IDisposable BeginRequestPipelineScope(ILogger logger, HttpRequestMessage request)
    //        {
    //            return _beginRequestPipelineScope(logger, request.Method, request.RequestUri, "");
    //        }

    //        public static void RequestPipelineStart(ILogger logger, HttpRequestMessage request)
    //        {
    //            _requestPipelineStart(logger, request.Method, request.RequestUri, "", null);
    //        }

    //        public static void RequestPipelineEnd(ILogger logger, HttpResponseMessage response)
    //        {
    //            _requestPipelineEnd(logger, response.StatusCode, null);
    //        }
    //    }
    //}
}