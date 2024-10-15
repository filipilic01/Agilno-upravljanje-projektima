using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Data.Descriptions
{
    public interface IDescriptionRepository: IRepository<Description>
    {
        Task<DescriptionConfirmation> CreateDescriptionAsync(Description create);
    }
}
