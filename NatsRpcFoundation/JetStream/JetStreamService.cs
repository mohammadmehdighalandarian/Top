using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;

using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Internal;

namespace NatsRpcFoundation.JetStream;

public sealed class JetStreamService : IJetStreamService
{
    private readonly INatsJSContext _jetStream;

    public JetStreamService(INatsConnection connection)
    {
        _jetStream = connection.CreateJetStreamContext();
    }

    public ValueTask<INatsJSStream> CreateOrUpdateStreamAsync(
        string streamName,
        ICollection<string> subjects,
        TimeSpan? maxAge = null,
        long maxMsgs = -1,
        long maxBytes = -1,
        int replicas = 1,
        CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullOrWhiteSpace(streamName, nameof(streamName));
        ArgumentNullException.ThrowIfNull(subjects);
        Guard.AgainstOutOfRange(replicas, nameof(replicas), 1);

        var config = new StreamConfig(streamName, subjects)
        {
            MaxAge = maxAge ?? TimeSpan.Zero,
            MaxMsgs = maxMsgs,
            MaxBytes = maxBytes,
            NumReplicas = replicas
        };

        return _jetStream.CreateOrUpdateStreamAsync(config, cancellationToken);
    }

    public ValueTask<INatsJSConsumer> CreateOrUpdateConsumerAsync(
        string streamName,
        string consumerName,
        string? filterSubject = null,
        CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullOrWhiteSpace(streamName, nameof(streamName));
        Guard.AgainstNullOrWhiteSpace(consumerName, nameof(consumerName));

        var config = new ConsumerConfig(consumerName)
        {
            FilterSubject = filterSubject
        };

        return _jetStream.CreateOrUpdateConsumerAsync(streamName, config, cancellationToken);
    }

    public ValueTask<PubAckResponse> PublishAsync<T>(
    string subject,
    T data,
    CancellationToken cancellationToken = default)
    {
        return _jetStream.PublishAsync(subject, data, cancellationToken: cancellationToken);
    }

    public ValueTask<NatsResult<PubAckResponse>> TryPublishAsync<T>(
       string subject,
       T data,
       CancellationToken cancellationToken = default)
    {
        return _jetStream.TryPublishAsync(subject, data, cancellationToken: cancellationToken);
    }

    public IAsyncEnumerable<string> ListStreamNamesAsync(
        string? subjectFilter = null,
        CancellationToken cancellationToken = default) =>
        _jetStream.ListStreamNamesAsync(subjectFilter, cancellationToken);

    public IAsyncEnumerable<INatsJSConsumer> ListConsumersAsync(
        string streamName,
        CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullOrWhiteSpace(streamName, nameof(streamName));
        return _jetStream.ListConsumersAsync(streamName, cancellationToken);
    }

    public ValueTask<bool> DeleteStreamAsync(string streamName, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullOrWhiteSpace(streamName, nameof(streamName));
        return _jetStream.DeleteStreamAsync(streamName, cancellationToken);
    }

    public ValueTask<StreamPurgeResponse> PurgeStreamAsync(
        string streamName,
        string? filterSubject = null,
        CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullOrWhiteSpace(streamName, nameof(streamName));
        return _jetStream.PurgeStreamAsync(
            streamName,
            new StreamPurgeRequest { Filter = filterSubject },
            cancellationToken);
    }
}
