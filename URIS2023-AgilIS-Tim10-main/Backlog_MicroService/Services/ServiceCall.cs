using Backlog_MicroService.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Backlog_MicroService.Services
{
    public class ServiceCall : IServiceCall
    {
        private readonly ILoggerService _loggerService;

        public ServiceCall(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        /// <summary>
        /// Metoda za slanje get zahteva
        /// </summary>
        /// <param name="url">Url putanja ka drugom servisu</param>

        /// <returns></returns>
        public async Task<List<BacklogItemDto>> SendGetRequestAsync(string url, string token)
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

                    List<BacklogItemDto> backlogItemsDto= JsonConvert.DeserializeObject<List<BacklogItemDto>>(content);
                    Console.WriteLine(backlogItemsDto);
                    return backlogItemsDto;



                }
                return default;
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "SendGetRequestAsync", $"Greška prilikom komunikacije sa drugim servisom iz servisa Javno Nadmetanje. Ciljani url: {url}", e);
                return default;
            }

        }
    }
}
