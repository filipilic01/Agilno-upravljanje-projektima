using Newtonsoft.Json;

namespace User_Service.Services
{
    public class ServiceCall<T> : IServiceCall<T>
    {
        private readonly ILoggerService _loggerService;

        public ServiceCall(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }


        public async Task<T> SendGetRequestAsync(string url)
        {
            try
            {
                using var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");


                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }

                    return JsonConvert.DeserializeObject<T>(content);
                }
                return default;
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "SendGetRequestAsync", $"Error during communication between BacklogItem service and other services. URL: {url}", e);
                return default;
            }

        }
    }
}
