using Backlog_MicroService.Entities;

namespace Backlog_MicroService.Data
{
    public interface IProductBacklogRepository
    {
        List<ProductBacklog> GetProductBacklogs();
        public ProductBacklog GetProductBacklogId(Guid Id);
        public void RemoveProductBacklog(Guid Id);
        public ProductBacklogConfirmation AddProductBacklog(ProductBacklog productBacklog);
        public ProductBacklog UpdateProductBacklog(ProductBacklog productBacklog);
        public bool SaveChanges();
    }
}
