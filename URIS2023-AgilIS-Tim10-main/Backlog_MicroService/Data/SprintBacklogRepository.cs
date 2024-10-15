using AutoMapper;
using Backlog_MicroService.Entities;

namespace Backlog_MicroService.Data
{
    public class SprintBacklogRepository : ISprintBacklogRepository
    {

        private readonly BacklogDBContext context;
        private readonly IMapper mapper;

        public SprintBacklogRepository (BacklogDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public SprintBacklogConfirmation AddSprintBacklog(SprintBacklog sprintBacklog)
        {
            var createdSprintBacklog = context.Add(sprintBacklog);
            return mapper.Map<SprintBacklogConfirmation>(createdSprintBacklog.Entity);
        }

        public SprintBacklog GetSprintBacklogId(Guid Id)
        {
            return context.SprintBacklogs.FirstOrDefault(e => e.IdBacklog == Id);
        }

        public List<SprintBacklog> GetSprintBacklogs()
        {
            List<SprintBacklog> sprintBacklogs = context.SprintBacklogs.ToList();
            return sprintBacklogs;
        }

        public void RemoveSprintBacklog(Guid Id)
        {
            var sprintBacklogs = GetSprintBacklogId(Id);
            context.Remove(sprintBacklogs);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public SprintBacklog UpdateSprintBacklog(SprintBacklog sprintBacklog)
        {
            try
            {
                var existingSprintBacklog = context.SprintBacklogs.FirstOrDefault(e => e.IdBacklog == sprintBacklog.IdBacklog);

                if (existingSprintBacklog != null)
                {
                    // Update the existing backlog with the new values
                    existingSprintBacklog.Pocetak = sprintBacklog.Pocetak;
                    existingSprintBacklog.Kraj = sprintBacklog.Kraj;
                    existingSprintBacklog.Cilj = sprintBacklog.Cilj;
                    existingSprintBacklog.Opis = sprintBacklog.Opis;
                    existingSprintBacklog.Naslov= sprintBacklog.Naslov;
                    //existingSprintBacklog.ProjekatId = sprintBacklog.ProjekatId;
                    //existingSprintBacklog.KorisnikId = sprintBacklog.KorisnikId;

                    context.SaveChanges(); // Save changes to the database

                    return existingSprintBacklog;
                }
                else
                {
                    throw new KeyNotFoundException($"Backlog with ID {sprintBacklog.IdBacklog} not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception("Error updating backlog", ex);
            };
        }
    }
}
