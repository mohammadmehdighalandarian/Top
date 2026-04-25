using System.Globalization;

namespace TopinLite.Infra.Common.Utilities
{
    public static class ResponseCodeParser
    {
        public static decimal ParseDecimalOrFallback(string? input, decimal fallback)
        {
            return decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsed)
                ? parsed
                : fallback;
        }
    }
}
