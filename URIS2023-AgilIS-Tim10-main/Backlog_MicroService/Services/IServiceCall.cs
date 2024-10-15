using Backlog_MicroService.Models;

namespace Backlog_MicroService.Services
{
    public interface IServiceCall
    {
        Task<List<BacklogItemDto>> SendGetRequestAsync(string url, string token);
    }
}
