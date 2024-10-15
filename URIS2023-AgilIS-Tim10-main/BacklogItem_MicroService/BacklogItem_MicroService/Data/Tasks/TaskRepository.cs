using AutoMapper;
using BacklogItem_MicroService.Models.Configurations;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogItem_MicroService.Data.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TaskConfirmation> CreateTaskAsync(TaskEntity create)
        {
            var createdEntity = await _context.Tasks.AddAsync(create);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            _context.Tasks.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskEntity>> GetAllAsync()
        {
            var items = await _context.Tasks.ToListAsync();
            return items;
        }

        public async Task<TaskEntity> GetByIdAsync(Guid id)
        {
            return _context.Tasks.FirstOrDefault(e => e.BacklogItemId == id);
        }

        public Task<List<TaskEntity>> GetTasksByBacklogIdAsync(Guid id)
        {
            return _context.Tasks.Where(e => e.BacklogId == id).ToListAsync();
        }

        public async Task UpdateAsync(TaskEntity update)
        {
            var item = await _context.Tasks.FindAsync(update.BacklogItemId);
            if (item != null)
            {
                item.BacklogItemName = update.BacklogItemName;


                await _context.SaveChangesAsync();
            }
        }
    }
}
