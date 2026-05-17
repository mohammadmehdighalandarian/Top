using System.Text.Json;
using Microsoft.Extensions.Primitives;

namespace TopinLite.Api.Edge.Middleware
{
    public partial class UserActivityLog
    {
        public string ConvertHttpHeadersToString(IHeaderDictionary headerDictionary)
        {
            Dictionary<string, StringValues> headerList = headerDictionary.ToDictionary(t => t.Key, t => t.Value);
            return JsonSerializer.Serialize(headerList);
        }

        private static string ReadStreamInChunks(Stream stream)
        {

            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using StringWriter textWriter = new();
            using StreamReader reader = new(stream);

            char[] readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        public long GetUnixTimeNow()
        {
            DateTimeOffset dto = new(DateTime.Now);
            return dto.ToUnixTimeMilliseconds();
        }
    }
}
