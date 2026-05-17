using System.Globalization;
using System.Security.Cryptography;
using TopinLite.Services.Commons;

namespace TopinLite.Infra.Common.Services
{
    public class ProviderIdGenerator : IProviderIdGenerator
    {
        private static int _currentSequenceValue = 4360;
        private static readonly object _sequenceLock = new object();
        public decimal Generate()
        {
            var now = DateTime.Now;

            string formatted =
                  now.ToString("MM")
                + now.ToString("ss")
                + now.ToString("dd")
                + now.ToString("mm")
                + now.ToString("HH")
                + (now.Year % 10);

            return decimal.Parse($"1{formatted}{GenerateRandomDigits(4)}");
        }

        public decimal GeneratePackageProviderId()
        {

            DateTime now = DateTime.Now;

            string timestamp = now.ToString("mm", CultureInfo.InvariantCulture) +   // minutes
                               now.ToString("ss", CultureInfo.InvariantCulture) +   // seconds
                               now.ToString("dd", CultureInfo.InvariantCulture) +   // day
                               now.ToString("mm", CultureInfo.InvariantCulture) +   // minutes again (repeated!)
                               now.ToString("HH", CultureInfo.InvariantCulture) +   // hour (24-hour)
                               (now.Year % 10).ToString(CultureInfo.InvariantCulture); // last digit of year

            // Get the next sequence value (with increment by 3 and wrap-around)
            int nextSequence = GetNextSequenceValue();

            // Combine: prefix "2" + timestamp + sequence
            string result = $"2{timestamp}{nextSequence}";

            return decimal.Parse(result, CultureInfo.InvariantCulture);
        }

        private static string GenerateRandomDigits(int length)
        {
            byte[] bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes);

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
                chars[i] = (char)('0' + (bytes[i] % 10));

            return new string(chars);
        }

        private static int GetNextSequenceValue()
        {
            lock (_sequenceLock)
            {
                int current = _currentSequenceValue;

                // Increment by 3 (matches Oracle: increment by 3)
                _currentSequenceValue += 3;

                // Check for wrap-around (matches Oracle: cycle)
                if (_currentSequenceValue > 9999)  // maxvalue 9999
                {
                    _currentSequenceValue = 1000;  // minvalue 1000
                }

                // Also handle if we exceed max during increment
                if (current > 9999)
                {
                    current = 1000;
                    _currentSequenceValue = 1003;
                }

                return current;
            }
        }

    }
}
