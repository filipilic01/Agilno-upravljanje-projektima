using AutoMapper;
using Rag_MicroService.Models;
using Rag_MicroService.Models.Entities;

namespace Rag_MicroService.Data
{
    public class RagRepository : IRagRepository

    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RagRepository(ApplicationDbContext context, IMapper mapper) {
        
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public RagConfirmation CreateRag(Rag rag)
        {
            var createdRag = context.Add(rag);
            context.SaveChanges();
            return mapper.Map<RagConfirmation>(createdRag.Entity);
        }

        public void DeleteRag(Guid id)
        {
            var deletedRag = GetRagById(id);
            context.Remove(deletedRag);
            context.SaveChanges();
        }

        public Rag GetRagById(Guid id)
        {
            return context.Rags.FirstOrDefault(e => e.RagId == id);
        }

        public List<Rag> GetRags()
        {
            return context.Rags.ToList();
        }

        public void UpdateRag(Rag rag)
        {
            var oldRag = context.Rags.FirstOrDefault(e => e.RagId == rag.RagId);

            if (oldRag != null)
            {

                oldRag.RagValue = rag.RagValue;
               
                context.SaveChanges();
            }
        }

        public Rag GetRagByBacklogItemId(Guid id)
        {
            return context.Rags.FirstOrDefault(e => e.BacklogItemId == id);
        }
    }
}
