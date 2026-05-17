using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Configuration;
using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.infra.PostgreSQL.Services;

internal sealed class HttpTrafficBackgroundWriter : BackgroundService
{
    private readonly IHttpTrafficLogQueue _queue;
    private readonly IHttpTrafficRepository _repository;
    private readonly ILogger<HttpTrafficBackgroundWriter> _logger;
    private readonly PostgresTrafficOptions _options;

    public HttpTrafficBackgroundWriter(
        IHttpTrafficLogQueue queue,
        IHttpTrafficRepository repository,
        ILogger<HttpTrafficBackgroundWriter> logger,
        IOptions<PostgresTrafficOptions> options)
    {
        _queue = queue;
        _repository = repository;
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var buffer = new List<HttpTrafficLogItem>(_options.BatchSize);
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(Math.Max(100, _options.FlushIntervalMilliseconds)));

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                while (buffer.Count < _options.BatchSize && !stoppingToken.IsCancellationRequested)
                {
                    var readTask = _queue.DequeueAsync(stoppingToken).AsTask();
                    var tickTask = timer.WaitForNextTickAsync(stoppingToken).AsTask();

                    var completed = await Task.WhenAny(readTask, tickTask).ConfigureAwait(false);

                    if (completed == readTask)
                    {
                        buffer.Add(await readTask.ConfigureAwait(false));
                    }
                    else
                    {
                        break;
                    }
                }

                if (buffer.Count > 0)
                {
                    await FlushAsync(buffer, stoppingToken).ConfigureAwait(false);
                    buffer.Clear();
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                if (_options.LogRepositoryErrors)
                {
                    _logger.LogError(ex, "Error while writing HTTP traffic logs to PostgreSQL.");
                }

                buffer.Clear();
                await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
            }
        }

        if (buffer.Count > 0)
        {
            try
            {
                await FlushAsync(buffer, CancellationToken.None).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (_options.LogRepositoryErrors)
                {
                    _logger.LogError(ex, "Error while flushing remaining HTTP traffic logs to PostgreSQL.");
                }
            }
        }
    }

    private Task FlushAsync(List<HttpTrafficLogItem> buffer, CancellationToken cancellationToken)
    {
        return _repository.BulkInsertTrafficAsync(buffer, cancellationToken);
    }
}
