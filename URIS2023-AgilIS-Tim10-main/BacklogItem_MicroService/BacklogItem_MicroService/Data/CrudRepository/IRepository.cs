namespace BacklogItem_MicroService.Data.CrudRepository
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task UpdateAsync(T update);

        Task DeleteAsync(Guid id);

        
    }
}
