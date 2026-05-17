using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TopinLite.Domain.LogModels
{
    public class ErrorLogModel
    {
        public ErrorLogModel(Exception e, HttpContext context)
        {
            try
            {
                JsonSerializerSettings settings = new()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.None,
                    MaxDepth = 2,
                };

                if (context is not null)
                {
                    HttpContextJson = new HttpContextJsonModel
                    {
                        Header = JsonConvert.SerializeObject(context.Request.Headers, settings),
                        Path = context.Request.Path,
                        QueryParam = context.Request.QueryString.ToString()
                    };
                }

                ExceptionMessage = e?.Message?.Length > 4000 ? e.Message[..4000] : e?.Message ?? "";
                StackTrace = e?.StackTrace?.Length > 4000 ? e.StackTrace[..4000] : e?.StackTrace ?? "";
                ExceptionJson = e == null ? "" : JsonConvert.SerializeObject(e, settings);
            }
            catch (Exception ex)
            {
                ExceptionMessage = "خطا در ساخت مدل لاگ: " + ex.Message;
            }
        }


        public HttpContextJsonModel HttpContextJson { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string ExceptionJson { get; set; }
    }
    
    public class HttpContextJsonModel
    {
        public string Header { get; set; }
        public string QueryParam { get; set; }
        public string Path { get; set; }
    }
}