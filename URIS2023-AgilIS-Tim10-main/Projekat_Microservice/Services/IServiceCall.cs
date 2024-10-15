using Projekat_Microservice.Models;

namespace Projekat_Microservice.Services
{
    public interface IServiceCall
    {
        Task<List<BacklogDto>> SendGetRequestAsync(string url, string token);
    }
}
