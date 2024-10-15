using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BacklogItem_MicroService.Data.UserStories
{
    public interface IUserStoryRepository: IRepository<UserStory>
    {
        Task<UserStoryConfirmation> CreateUserStoryAsync(UserStory create);

        Task<List<UserStory>> GetUserStoriesByBacklogIdAsync(Guid id);
    }
}
