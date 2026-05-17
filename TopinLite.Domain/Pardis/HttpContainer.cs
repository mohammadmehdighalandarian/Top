namespace TopinLite.Domain.Pardis
{
    public class HttpContainer<T>
    {
        public T HttpResponse { get; set; }

        public int HttpStatusCode { get; set; }
    }
}
