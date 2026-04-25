using System.Globalization;

namespace TopinLite.Infra.Common.Utilities
{
    public static class MsisdnNormalizer
    {
        public static string Normalize(decimal msisdn)
        {
            return decimal.Truncate(msisdn).ToString("0", CultureInfo.InvariantCulture);
        }
    }
}
