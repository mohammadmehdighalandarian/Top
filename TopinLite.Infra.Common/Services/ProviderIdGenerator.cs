using System.Security.Cryptography;
using TopinLite.Services.Commons;

namespace TopinLite.Infra.Common.Services
{
    public class ProviderIdGenerator : IProviderIdGenerator
    {
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

        private static string GenerateRandomDigits(int length)
        {
            byte[] bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes);

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
                chars[i] = (char)('0' + (bytes[i] % 10));

            return new string(chars);
        }
    }
}
