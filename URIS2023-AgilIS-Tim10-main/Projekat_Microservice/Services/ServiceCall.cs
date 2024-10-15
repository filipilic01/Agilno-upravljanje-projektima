using Newtonsoft.Json;
using Projekat_Microservice.Models;
using System.Net.Http.Headers;

namespace Projekat_Microservice.Services
{
    public class ServiceCall : IServiceCall
    {
        private readonly ILoggerService loggerService;
        public ServiceCall (ILoggerService _loggerService)
        {
            loggerService = _loggerService;
        }
        public async Task<List<BacklogDto>> SendGetRequestAsync(string url, string token)
        {
            try
            {
                using var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }

                    List<BacklogDto> backlogDto = JsonConvert.DeserializeObject<List<BacklogDto>>(content);
                    Console.WriteLine(backlogDto);
                    return backlogDto;
                }
                return default;
            }
            catch (Exception e)
            {
                await loggerService.Log(LogLevel.Error, "SendGetRequestAsync", $"Error during communication between Tim service and other services. URL: {url}", e);
                return default;
            }
        }
    }
}

