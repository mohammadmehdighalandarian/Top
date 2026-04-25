using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using NatsRpcFoundation.Dispatching;

namespace NatsRpcFoundation.Hosting;

public sealed class RpcResponderBackgroundService : BackgroundService
{
    private readonly INatsConnection _connection;
    private readonly IServiceProvider _serviceProvider;
    private readonly IRpcSerializer _serializer;
    private readonly IReadOnlyList<RpcHandlerRegistration> _registrations;
    private readonly ILogger<RpcResponderBackgroundService> _logger;
    private readonly ConcurrentDictionary<string, RpcHandlerExecutor> _executors = new(StringComparer.Ordinal);

    public RpcResponderBackgroundService(
        INatsConnection connection,
        IServiceProvider serviceProvider,
        IRpcSerializer serializer,
        IEnumerable<RpcHandlerRegistration> registrations,
        ILogger<RpcResponderBackgroundService> logger)
    {
        _connection = connection;
        _serviceProvider = serviceProvider;
        _serializer = serializer;
        _registrations = registrations.ToList();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach (var reg in _registrations)
        {
            _executors.TryAdd(reg.Subject, RpcHandlerExecutorFactory.Create(reg, _serializer));
        }

        var tasks = _registrations.Select(r => RunSubscriptionAsync(r, stoppingToken)).ToArray();
        await Task.WhenAll(tasks);
    }

    private Task RunSubscriptionAsync(RpcHandlerRegistration registration, CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            var channel = Channel.CreateBounded<NatsMsg<byte[]>>(new BoundedChannelOptions(Math.Max(8, registration.MaxConcurrency * 8))
            {
                SingleWriter = true,
                SingleReader = false,
                FullMode = BoundedChannelFullMode.Wait
            });

            var workers = Enumerable.Range(0, registration.MaxConcurrency)
                .Select(_ => Task.Run(() => WorkerLoopAsync(registration, channel.Reader, stoppingToken), stoppingToken))
                .ToArray();

            try
            {
                await foreach (var msg in _connection.SubscribeAsync<byte[]>(registration.Subject, cancellationToken: stoppingToken))
                {
                    await channel.Writer.WriteAsync(msg, stoppingToken);
                }
            }
            finally
            {
                channel.Writer.TryComplete();
                await Task.WhenAll(workers);
            }
        }, stoppingToken);
    }

    private async Task WorkerLoopAsync(
        RpcHandlerRegistration registration,
        ChannelReader<NatsMsg<byte[]>> reader,
        CancellationToken cancellationToken)
    {
        await foreach (var msg in reader.ReadAllAsync(cancellationToken))
        {
            await ProcessMessageAsync(registration, msg, cancellationToken);
        }
    }

    private async Task ProcessMessageAsync(
        RpcHandlerRegistration registration,
        NatsMsg<byte[]> msg,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(msg.ReplyTo))
            return;

        if (!_executors.TryGetValue(registration.Subject, out var executor))
            return;

        try
        {
            if (msg.Data is null || msg.Data.Length == 0)
            {
                await PublishReplyAsync(msg.ReplyTo, RpcResult<object>.Fail("EMPTY_REQUEST", "Request body is empty."), cancellationToken);
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var handler = executor.ResolveHandler(scope.ServiceProvider);

            var context = new RpcContext
            {
                Subject = registration.Subject,
                ReceivedAtUtc = DateTimeOffset.UtcNow
            };

            var responseBytes = await executor.ExecuteAsync(handler, msg.Data, context, cancellationToken);
            await _connection.PublishAsync(msg.ReplyTo, responseBytes, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RPC handler failed for subject {Subject}", registration.Subject);
            await PublishReplyAsync(
                msg.ReplyTo!,
                RpcResult<object>.Fail("UNHANDLED_ERROR", ex.GetBaseException().Message),
                cancellationToken);
        }
    }

    private Task PublishReplyAsync<T>(string replyTo, T value, CancellationToken cancellationToken)
    {
        var bytes = _serializer.Serialize(value);
        return _connection.PublishAsync(replyTo, bytes, cancellationToken: cancellationToken).AsTask();
    }
}
