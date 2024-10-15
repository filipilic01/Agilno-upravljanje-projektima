namespace User_Service.Services
{
    public interface ILoggerService
    {
        Task<bool> Log(LogLevel level, string method, string message, Exception error = null);

    }
}
