using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;
using System.Net.Http.Headers;
using Tim_Microservice.VO;

namespace Tim_Microservice.Services
{
    public class ServiceCall : IServiceCall
    {
        private readonly ILoggerService _loggerService;

        public ServiceCall(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public async Task<List<Projekat>> SendGetRequestAsync(string url, string token)
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

                    return JsonConvert.DeserializeObject<List<Projekat>>(content);
                }
                return default;
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "SendGetRequestAsync", $"Error during communication between Tim service and other services. URL: {url}", e);
                return default;
            }
        }
    }
}
