namespace FAQsection_MicroService.Services
{
    public interface ILoggerService
    {
        Task<bool> Log(LogLevel level, string method, string message, Exception error = null);
    }
}
