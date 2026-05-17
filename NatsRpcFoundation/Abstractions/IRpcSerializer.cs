namespace NatsRpcFoundation.Abstractions;

public interface IRpcSerializer
{
    byte[] Serialize<T>(T value);
    T Deserialize<T>(ReadOnlySpan<byte> data);
}
