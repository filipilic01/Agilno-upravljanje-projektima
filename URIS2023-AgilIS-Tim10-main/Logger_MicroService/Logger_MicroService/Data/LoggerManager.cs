using NLog;

namespace Logger_MicroService.Data
{
    public class LoggerManager : ILoggerManager
    {
        private static NLog.ILogger _logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(Exception e, string message)
        {
            _logger.Error(e, message);
        }

        public void LogInformation(string message)
        {
            _logger.Info(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }
    }
}
