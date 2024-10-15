using BacklogItem_MicroService.Models.OtherModelServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BacklogItem_MicroService.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration _configuration;

        public LoggerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> Log(LogLevel level, string method, string message, Exception error = null)
        {
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    
                    string url = _configuration.GetValue<string>("Services:LoggerService");
                    Console.WriteLine(url);
                    var log = new Log
                    {
                        Level = level,
                        Message = message,
                        Error = error ?? new Exception("UnknownError"),
                        Service = "BacklogItem_Service",
                        Method = method
                    };
                    string logJson = JsonConvert.SerializeObject(log);
                    Console.WriteLine(logJson);
                    HttpContent content = new StringContent(logJson);
                    Console.WriteLine(content);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response =  httpClient.PostAsync(url, content).Result;
                    Console.WriteLine(response);
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Content: {response.Content.ReadAsStringAsync().Result}");
                    return await Task.FromResult(response.IsSuccessStatusCode);

                }

            }
            catch(AggregateException e)
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
