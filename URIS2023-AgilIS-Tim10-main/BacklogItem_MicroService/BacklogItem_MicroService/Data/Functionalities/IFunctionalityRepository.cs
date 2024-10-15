using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Data.Functionalities
{
    public interface IFunctionalityRepository: IRepository<Functionality>
    {
        Task<FunctionalityConfirmation> CreateFunctionalityAsync(Functionality create);

        Task<List<Functionality>> GetFunctionalitiesByBacklogIdAsync(Guid id);
    }
}
