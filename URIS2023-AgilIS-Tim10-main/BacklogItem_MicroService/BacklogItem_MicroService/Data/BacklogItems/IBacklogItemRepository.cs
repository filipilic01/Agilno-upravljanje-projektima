using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Data.BacklogItems
{
    public interface IBacklogItemRepository: IRepository<BacklogItem>
    {
        Task<BacklogItemConfirmation> CreateBacklogItemAsync(BacklogItem create);

        Task<AcceptanceCriteria> GetAcceptanceCriteriaByBacklogItemIdAsync(Guid id);
        Task<Description> GetDescriptionByBacklogItemIdAsync(Guid id);
        Task<StoryPoint> GetStoryPointByBacklogItemIdAsync(Guid id);

        Task<List<BacklogItem>> GetBacklogItemsByBacklogIdAsync(Guid id);

    }
}
