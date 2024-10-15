using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Data.StoryPoints
{
    public interface IStoryPointRepository: IRepository<StoryPoint>
    {
        Task<StoryPointConfirmation> CreateStoryPointAsync(StoryPoint create);
    }
}
