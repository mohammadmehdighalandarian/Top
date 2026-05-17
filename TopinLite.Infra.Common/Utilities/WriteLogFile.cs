using log4net;
using Newtonsoft.Json;

namespace TopinLite.Infra.Common.Utilities;

public static class WriteLogFile
{
    #region Constructor
    private static readonly ILog _logger;

    static WriteLogFile()
    {

        _logger = LogManager.GetLogger(typeof(WriteLogFile));
    }
    #endregion
    
    public static void LogFile(string title, object text)
    {
        try
        {
            string formattedMessage =
                $"{title} : {DateTime.Now:yyyy/MM/dd HH:mm:ss:fff} :" + Environment.NewLine +
                ":::::::::::::::::::::::::::::::::" + Environment.NewLine +
                JsonConvert.SerializeObject(text, Newtonsoft.Json.Formatting.Indented) + Environment.NewLine +
                ":::::::::::::::::::::::::::::::::";

            _logger.Error(formattedMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception : " + ex.Message);
            Console.WriteLine(title + " : " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " : ");
            Console.WriteLine(":::::::::::::::::::::::::::::::::");
            Console.WriteLine(JsonConvert.SerializeObject(text, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine(":::::::::::::::::::::::::::::::::");
        }
    }

    public static void LogFileJSON(string treeId, object text)
    {
        string json = JsonConvert.SerializeObject(new
        {
            TreeId = treeId,
            Time = DateTime.Now,
            Data = text
        }, Newtonsoft.Json.Formatting.Indented);

        string message =
            $"JsonLog : {DateTime.Now:yyyy/MM/dd HH:mm:ss:fff}\n" +
            ":::::::::::::::::::::::::::::::::\n" +
            json +
            "\n:::::::::::::::::::::::::::::::::\n";

        _logger.Info(message);
    }
}