using Backlog_MicroService.Entities;
using Backlog_MicroService.Models.SprintBacklog;

namespace Backlog_MicroService.Data
{
    public interface ISprintBacklogRepository
    {
        List<SprintBacklog> GetSprintBacklogs();
        public SprintBacklog GetSprintBacklogId(Guid Id);
        public void RemoveSprintBacklog(Guid Id);
        public SprintBacklogConfirmation AddSprintBacklog(SprintBacklog sprintBacklog);
        public SprintBacklog UpdateSprintBacklog(SprintBacklog sprintBacklog);
        public bool SaveChanges();
    }
}
