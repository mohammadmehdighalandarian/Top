using NATS.Client.Core;
using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using NatsRpcFoundation.Internal;

namespace NatsRpcFoundation.Services;

public sealed class RpcClient : IRpcClient
{
    private readonly INatsConnection _connection;
    private readonly IRpcSerializer _serializer;

    public RpcClient(INatsConnection connection, IRpcSerializer serializer)
    {
        _connection = connection;
        _serializer = serializer;
    }

    public async Task<RpcResult<TResponse>> RequestAsync<TRequest, TResponse>(
        string subject,
        TRequest request,
        RpcCallOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullOrWhiteSpace(subject, nameof(subject));

        options ??= new RpcCallOptions {TraceId= Guid.NewGuid().ToString("N"),Timeout= TimeSpan.FromSeconds(10) };

        var envelope = new RpcEnvelope<TRequest>
        {
            CorrelationId = options.CorrelationId ?? Guid.NewGuid().ToString("N"),
            TraceId = options.TraceId,
            TenantId = options.TenantId,
            MessageType = typeof(TRequest).FullName,
            Headers = options.Headers,
            Payload = request
        };

        var requestBytes = _serializer.Serialize(envelope);

        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutCts.CancelAfter(options.Timeout);

        try
        {
            var msg = await _connection.RequestAsync<byte[], byte[]>(
                subject: subject,
                data: requestBytes,
                cancellationToken: timeoutCts.Token);

            if (msg.Data is null || msg.Data.Length == 0)
                throw new RpcTransportException($"Empty reply received for subject '{subject}'.");

            return _serializer.Deserialize<RpcResult<TResponse>>(msg.Data);
        }
        catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            throw new RpcTransportException(
                $"Request timeout after {options.Timeout.TotalMilliseconds:0} ms on subject '{subject}'.",
                ex);
        }
        catch (RpcTransportException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcTransportException($"NATS request failed for subject '{subject}'.", ex);
        }
    }
}
