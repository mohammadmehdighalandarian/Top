namespace TopinLite.Domain.Configuration
{
    public class RabbitMqConnectionConfigModel
    {
        public string HostAddress { get; set; }
        public int PortNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientName { get; set; }
    }
}