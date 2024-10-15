using AutoMapper;
using Backlog_MicroService.Entities;
using Backlog_MicroService.Models.Backlog;
using Microsoft.EntityFrameworkCore;

namespace Backlog_MicroService.Data
{
    public class BacklogRepository : IBacklogRepository
    {
        private readonly BacklogDBContext context;
        private readonly IMapper mapper;

        public BacklogRepository(BacklogDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Backlog> GetBacklogs()
        {
            var backlog = context.Backlogs.ToList();
            return backlog;
        }

        BacklogConfirmation IBacklogRepository.AddBacklog(Backlog backlog)
        {
            var createdBacklog = context.Backlogs.Add(backlog);
            return mapper.Map<BacklogConfirmation>(createdBacklog.Entity);
        }


        public Backlog GetBacklogId(Guid Id)
        {
            return context.Backlogs.FirstOrDefault(a => a.IdBacklog == Id);
        }

        public void RemoveBacklog(Guid Id)
        {
            var backlog = GetBacklogId(Id);
            context.Remove(backlog);
        }

        public Backlog UpdateBacklog(Backlog backlog)
        {
            try
            {
                var existingBacklog = context.Backlogs.FirstOrDefault(e => e.IdBacklog == backlog.IdBacklog);

                if (existingBacklog != null)
                {
             
                    existingBacklog.Naslov = backlog.Naslov;
                    existingBacklog.Opis = backlog.Opis;

                    context.SaveChanges(); // Save changes to the database

                    return existingBacklog;
                }
                else
                {
                    throw new KeyNotFoundException($"Backlog with ID {backlog.IdBacklog} not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception("Error updating backlog", ex);
            }
        }


        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Backlog GetProductBacklogByKorisnikId(Guid id)
        {
            return context.ProductBacklogs.FirstOrDefault(e => e.KorisnikId == id);
        }

        public Backlog GetSprintBacklogByKorisnikId(Guid id)
        {
            return context.SprintBacklogs.FirstOrDefault(e => e.KorisnikId == id);
        }

        public SprintBacklog GetSprintBacklogByProjekatId(Guid id)
        {
            return context.SprintBacklogs.FirstOrDefault(e => e.ProjekatId == id);
        }

        public ProductBacklog GetProductBacklogByProjekatId(Guid id)
        {
            return context.ProductBacklogs.FirstOrDefault(e => e.ProjekatId == id);
        }
    }
}
