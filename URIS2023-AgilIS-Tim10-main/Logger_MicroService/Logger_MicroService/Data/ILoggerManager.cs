namespace Logger_MicroService.Data
{
    public interface ILoggerManager
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(Exception e, string message);
        void LogDebug(string message);

    }
}
