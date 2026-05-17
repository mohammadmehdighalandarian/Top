namespace TopinLite.Infra.Common.Utilities
{
    public static class TokenParser
    {
        public static string GetToken(string theList, int theIndex, string delim = "|")
        {
            if (string.IsNullOrEmpty(theList) || theIndex < 1)
                return "";

            string[] parts = theList.Split([delim], StringSplitOptions.None);

            return theIndex > parts.Length ? "" : parts[theIndex - 1];
        }
    }
}
