using Newtonsoft.Json;
using System.Net.Http.Headers;
using User_Service.Models;

namespace User_Service.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public LoggerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<bool> Log(LogLevel level, string method, string message, Exception error = null)
        {
            try
            {
                
                    string url = _configuration.GetValue<string>("Services:LoggerService");
                    Console.WriteLine(url);
                    var log = new Log
                    {
                        Level = level,
                        Message = message,
                        Error = error ?? new Exception("UnknownError"),
                        Service = "User_Service",
                        Method = method
                    };
                    string logJson = JsonConvert.SerializeObject(log);
                    Console.WriteLine(logJson);
                    HttpContent content = new StringContent(logJson);
                    Console.WriteLine(content);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                    Console.WriteLine(response);
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                   
                    return response.IsSuccessStatusCode;
                
            }
            catch (AggregateException e)
            {
                foreach (var innerException in e.InnerExceptions)
                {
                    Console.WriteLine(innerException.Message);
                }
                return false;
            }
        }
    }
}
