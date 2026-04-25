using System.Text.Json;
using NatsRpcFoundation.Abstractions;

namespace NatsRpcFoundation.Serialization;

public sealed class SystemTextJsonRpcSerializer : IRpcSerializer
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = false,
        WriteIndented = false
    };

    public byte[] Serialize<T>(T value)
        => JsonSerializer.SerializeToUtf8Bytes(value, Options);

    public T Deserialize<T>(ReadOnlySpan<byte> data)
    {
        var result = JsonSerializer.Deserialize<T>(data, Options);
        if (result is null)
            throw new InvalidOperationException($"Failed to deserialize '{typeof(T).FullName}'.");
        return result;
    }
}
