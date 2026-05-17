namespace NatsRpcFoundation.Internal;

internal static class Guard
{
    public static void AgainstNull(object? value, string paramName)
    {
        if (value is null)
            throw new ArgumentNullException(paramName);
    }

    public static void AgainstNullOrWhiteSpace(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", paramName);
    }

    public static void AgainstOutOfRange(int value, string paramName, int min)
    {
        if (value < min)
            throw new ArgumentOutOfRangeException(paramName, $"Value must be >= {min}.");
    }
}
