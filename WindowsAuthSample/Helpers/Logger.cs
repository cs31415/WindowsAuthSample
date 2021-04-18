using log4net;

namespace WindowsAuthSample.Helpers
{
    public class Logger
    {
        public static void LogDebug(string message)
        {
            ILog log = LogManager.GetLogger("Test");
            log.Debug(message);
        }
    }
}