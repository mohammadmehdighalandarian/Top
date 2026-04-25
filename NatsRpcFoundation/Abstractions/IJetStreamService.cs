using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;

namespace NatsRpcFoundation.Abstractions;

public interface IJetStreamService
{
    ValueTask<INatsJSStream> CreateOrUpdateStreamAsync(
        string streamName,
        ICollection<string> subjects,
        TimeSpan? maxAge = null,
        long maxMsgs = -1,
        long maxBytes = -1,
        int replicas = 1,
        CancellationToken cancellationToken = default);

    ValueTask<INatsJSConsumer> CreateOrUpdateConsumerAsync(
        string streamName,
        string consumerName,
        string? filterSubject = null,
        CancellationToken cancellationToken = default);

    ValueTask<PubAckResponse> PublishAsync<T>(
        string subject,
        T data,
        CancellationToken cancellationToken = default);

    public ValueTask<NatsResult<PubAckResponse>> TryPublishAsync<T>(
           string subject,
           T data,
           CancellationToken cancellationToken = default);

    IAsyncEnumerable<string> ListStreamNamesAsync(
        string? subjectFilter = null,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<INatsJSConsumer> ListConsumersAsync(
        string streamName,
        CancellationToken cancellationToken = default);

    ValueTask<bool> DeleteStreamAsync(string streamName, CancellationToken cancellationToken = default);

    ValueTask<StreamPurgeResponse> PurgeStreamAsync(
        string streamName,
        string? filterSubject = null,
        CancellationToken cancellationToken = default);
}
