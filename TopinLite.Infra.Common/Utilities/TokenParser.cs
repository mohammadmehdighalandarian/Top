namespace TopinLite.Infra.Common.Utilities
{
    public static class TokenParser
    {
        public static string GetToken(string? input, int oneBasedIndex, char delimiter)
        {
            if (string.IsNullOrWhiteSpace(input) || oneBasedIndex <= 0)
            {
                return string.Empty;
            }

            string[] parts = input.Split(delimiter);
            return parts.Length >= oneBasedIndex ? parts[oneBasedIndex - 1].Trim() : string.Empty;
        }
    }
}
