namespace TopinLite.Infra.MsBroker.RabbitMQ.Loggers
{
    public class HttpClientLogger : IRabbitMQBroker
    {
        public HttpClientLogger()
        {
        }

        public bool PartyLogger(PartyCallLoggingModel model)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}