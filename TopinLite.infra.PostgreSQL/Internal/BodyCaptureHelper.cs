using System.Text;

using Microsoft.AspNetCore.Http;

namespace TopinLite.infra.PostgreSQL.Internal;

internal static class BodyCaptureHelper
{
    public static async Task<string?> ReadRequestBodyAsync(HttpRequest request, int maxBytes, CancellationToken cancellationToken)
    {
        if (request.Body is null || maxBytes <= 0)
        {
            return null;
        }

       // request.EnableBuffering();

        var bytes = await ReadLimitedBytesAsync(request.Body, maxBytes, cancellationToken).ConfigureAwait(false);
        request.Body.Position = 0;

        return bytes.Length == 0 ? null : Encoding.UTF8.GetString(bytes);
    }

    public static async Task<string?> ReadResponseBodyAsync(MemoryStream responseBuffer, int maxBytes, CancellationToken cancellationToken)
    {
        if (maxBytes <= 0)
        {
            return null;
        }

        responseBuffer.Position = 0;
        var bytes = await ReadLimitedBytesAsync(responseBuffer, maxBytes, cancellationToken).ConfigureAwait(false);
        responseBuffer.Position = 0;

        return bytes.Length == 0 ? null : Encoding.UTF8.GetString(bytes);
    }

    private static async Task<byte[]> ReadLimitedBytesAsync(Stream stream, int maxBytes, CancellationToken cancellationToken)
    {
        var buffer = new byte[Math.Min(81920, maxBytes)];
        await using var output = new MemoryStream(capacity: Math.Min(maxBytes, 81920));

        var remaining = maxBytes;
        while (remaining > 0)
        {
            var read = await stream.ReadAsync(buffer.AsMemory(0, Math.Min(buffer.Length, remaining)), cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                break;
            }

            await output.WriteAsync(buffer.AsMemory(0, read), cancellationToken).ConfigureAwait(false);
            remaining -= read;
        }

        return output.ToArray();
    }
}
