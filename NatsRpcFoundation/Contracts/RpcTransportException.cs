namespace NatsRpcFoundation.Contracts;

public sealed class RpcTransportException : Exception
{
    public RpcTransportException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
