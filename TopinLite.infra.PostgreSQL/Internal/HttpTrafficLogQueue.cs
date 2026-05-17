using System.Threading.Channels;

using Microsoft.Extensions.Options;

using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Configuration;
using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.infra.PostgreSQL.Internal;

internal sealed class HttpTrafficLogQueue : IHttpTrafficLogQueue
{
    private readonly Channel<HttpTrafficLogItem> _channel;
    private readonly PostgresTrafficOptions _options;

    public HttpTrafficLogQueue(IOptions<PostgresTrafficOptions> options)
    {
        _options = options.Value;

        var boundedOptions = new BoundedChannelOptions(Math.Max(1, _options.ChannelCapacity))
        {
            SingleReader = true,
            SingleWriter = false,
            FullMode = _options.DropWhenChannelFull
                ? BoundedChannelFullMode.DropWrite
                : BoundedChannelFullMode.Wait
        };

        _channel = Channel.CreateBounded<HttpTrafficLogItem>(boundedOptions);
    }

    public bool TryEnqueue(HttpTrafficLogItem item)
    {
        return _channel.Writer.TryWrite(item);
    }

    public ValueTask<HttpTrafficLogItem> DequeueAsync(CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAsync(cancellationToken);
    }
}
