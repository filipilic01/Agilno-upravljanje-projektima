using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Data.Tasks
{
    public interface ITaskRepository: IRepository<TaskEntity>
    {
        Task<TaskConfirmation> CreateTaskAsync(TaskEntity create);
        Task<List<TaskEntity>> GetTasksByBacklogIdAsync(Guid id);
    }
}
