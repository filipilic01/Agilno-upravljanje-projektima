using Tim_Microservice.VO;

namespace Tim_Microservice.Services
{
    public interface IServiceCall
    {
        Task<List<Projekat>> SendGetRequestAsync(string url, string token);
    }
}
