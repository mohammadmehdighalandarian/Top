namespace TopinLite.Domain.Pardis
{
    public class PardisAuthResponseModel
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string session_state { get; set; }
        public string scope { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
