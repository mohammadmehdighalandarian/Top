namespace NatsRpcFoundation.Contracts;

public sealed class RpcResult<T>
{
    public bool Success { get; init; }
    public string? Code { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }

    public static RpcResult<T> Ok(T data) => new()
    {
        Success = true,
        Data = data
    };

    public static RpcResult<T> Fail(string code, string message) => new()
    {
        Success = false,
        Code = code,
        Message = message
    };
}
