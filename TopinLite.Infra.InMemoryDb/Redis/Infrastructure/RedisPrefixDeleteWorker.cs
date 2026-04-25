using TopinLite.Infra.InMemoryDb.Redis.Abstractions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace TopinLite.Infra.InMemoryDb.Redis.Infrastructure
{
    public sealed class RedisPrefixDeleteWorker : BackgroundService
    {
        private readonly RedisPrefixDeleteQueue _queue;
        private readonly IRedisPrefixDeleteService _service;
        private readonly ILogger<RedisPrefixDeleteWorker> _logger;

        public RedisPrefixDeleteWorker(
            RedisPrefixDeleteQueue queue,
            IRedisPrefixDeleteService service,
            ILogger<RedisPrefixDeleteWorker> logger)
        {
            _queue = queue;
            _service = service;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var request in _queue.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    var deleted = await _service.DeleteByPrefixAsync(request.Prefix, stoppingToken);
                    _logger.LogInformation(
                        "Background Redis prefix deletion completed. Prefix={Prefix}, Deleted={Deleted}",
                        request.Prefix, deleted);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Background Redis prefix deletion failed for prefix {Prefix}",
                        request.Prefix);
                }
            }
        }
    }
}