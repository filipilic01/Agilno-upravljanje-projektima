using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Data.Epics
{
    public interface IEpicRepository: IRepository<Epic>
    {
        Task<EpicConfirmation> CreateEpicAsync(Epic create);

        Task<List<Epic>> GetEpicsByBacklogIdAsync(Guid id);
    }
}
