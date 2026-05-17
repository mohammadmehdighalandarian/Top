using Microsoft.Extensions.Options;

using System.Threading.Channels;

using TopinLite.Infra.InMemoryDb.Redis.Abstractions;
using TopinLite.Infra.InMemoryDb.Redis.Configuration;
using TopinLite.Infra.InMemoryDb.Redis.Models;

using Microsoft.Extensions.Logging;

namespace TopinLite.Infra.InMemoryDb.Redis.Infrastructure
{
    public sealed class RedisPrefixDeleteQueue : IRedisPrefixDeleteQueue
    {
        private readonly Channel<PrefixDeleteRequest> _channel;
        private readonly RedisPrefixDeleteMetrics _metrics;
        private readonly ILogger<RedisPrefixDeleteQueue> _logger;

        public RedisPrefixDeleteQueue(
            IOptions<RedisPrefixDeleteOptions> options,
            RedisPrefixDeleteMetrics metrics,
            ILogger<RedisPrefixDeleteQueue> logger)
        {
            var capacity = options.Value.QueueCapacity <= 0 ? 500 : options.Value.QueueCapacity;

            _channel = Channel.CreateBounded<PrefixDeleteRequest>(new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleReader = true,
                SingleWriter = false
            });

            _metrics = metrics;
            _logger = logger;
        }

        public async ValueTask QueueAsync(PrefixDeleteRequest request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            await _channel.Writer.WriteAsync(request, cancellationToken);
            _metrics.QueuedRequests.Add(1);
            _logger.LogInformation("Queued Redis prefix delete request for prefix {Prefix}", request.Prefix);
        }

        public ChannelReader<PrefixDeleteRequest> Reader => _channel.Reader;
    }
}