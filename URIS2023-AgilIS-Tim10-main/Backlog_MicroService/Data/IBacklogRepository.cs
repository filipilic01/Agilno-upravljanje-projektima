using Backlog_MicroService.Entities;
using Backlog_MicroService.Models.Backlog;

namespace Backlog_MicroService.Data
{
    public interface IBacklogRepository
    {
        
        public List<Backlog> GetBacklogs();
        public Backlog GetBacklogId(Guid Id);
        public void RemoveBacklog(Guid Id);
        public BacklogConfirmation AddBacklog(Backlog backlog);
        public Backlog UpdateBacklog(Backlog backlog);
        public bool SaveChanges();

        public SprintBacklog GetSprintBacklogByProjekatId(Guid id);

        public ProductBacklog GetProductBacklogByProjekatId(Guid id);
        public Backlog GetProductBacklogByKorisnikId(Guid id);
        public Backlog GetSprintBacklogByKorisnikId(Guid id);
    }
}
