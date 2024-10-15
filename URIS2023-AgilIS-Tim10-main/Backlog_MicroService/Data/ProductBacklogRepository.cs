using AutoMapper;
using Backlog_MicroService.Entities;

namespace Backlog_MicroService.Data
{
    public class ProductBacklogRepository : IProductBacklogRepository
    {
        private readonly BacklogDBContext context;
        private readonly IMapper mapper;

        public ProductBacklogRepository(BacklogDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ProductBacklogConfirmation AddProductBacklog(ProductBacklog productBacklog)
        {
            var createdProductBacklog = context.Add(productBacklog);
            return mapper.Map<ProductBacklogConfirmation>(createdProductBacklog.Entity);
        }

        public ProductBacklog GetProductBacklogId(Guid Id)
        {
            return context.ProductBacklogs.FirstOrDefault(e => e.IdBacklog == Id);
        }

        public List<ProductBacklog> GetProductBacklogs()
        {
            List<ProductBacklog> productBacklogs = context.ProductBacklogs.ToList();
            return productBacklogs;
        }

        public void RemoveProductBacklog(Guid Id)
        {
            var productBacklogs = GetProductBacklogId(Id);
            context.Remove(productBacklogs);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ProductBacklog UpdateProductBacklog(ProductBacklog productBacklog)
        {
            try
            {
                var existingProductBacklog = context.ProductBacklogs.FirstOrDefault(e => e.IdBacklog == productBacklog.IdBacklog);

                if (existingProductBacklog != null)
                {
                    // Update the existing backlog with the new values
                    existingProductBacklog.OpisZadatak = productBacklog.OpisZadatak;
                    existingProductBacklog.VremeTrajanja = productBacklog.VremeTrajanja;
                    existingProductBacklog.AngazovaniRadnici = productBacklog.AngazovaniRadnici;
                    existingProductBacklog.Naslov = productBacklog.Naslov;
                    existingProductBacklog.Opis = productBacklog.Opis;
                    //existingProductBacklog.KorisnikId = productBacklog.KorisnikId;
                    //sexistingProductBacklog.ProjekatId = productBacklog.ProjekatId;

                    context.SaveChanges(); // Save changes to the database

                    return existingProductBacklog;
                }
                else
                {
                    throw new KeyNotFoundException($"Backlog with ID {productBacklog.IdBacklog} not found");
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
