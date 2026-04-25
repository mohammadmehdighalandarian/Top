using System.Globalization;

namespace TopinLite.Infra.Common.Utilities
{
    public static class DateParser
    {
        public static readonly string[] BirthDateFormats =
        [
            "MM/dd/yyyy",
            "MM/dd/yyyy HH:mm:ss",
            "MM/dd/yyyy hh:mm:ss tt",
            "yyyy/MM/dd",
            "yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss"
        ];

        public static readonly string[] SubscriberDateFormats =
        [
            "MM/dd/yyyy HH:mm:ss",
            "MM/dd/yyyy hh:mm:ss tt",
            "MM/dd/yyyy",
            "yyyy/MM/dd HH:mm:ss",
            "yyyy/MM/dd",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-dd"
        ];

        public static bool TryParse(string? input, out DateTime dateTime, params string[] formats)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                dateTime = default;
                return false;
            }

            string value = input.Trim();
            if (formats.Length > 0
                && DateTime.TryParseExact(
                    value,
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                    out dateTime))
            {
                return true;
            }

            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTime);
        }
    }
}
