using AutoMapper;
using BacklogItem_MicroService.Models.Configurations;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogItem_MicroService.Data.Epics
{
    public class EpicRepository : IEpicRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EpicRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EpicConfirmation> CreateEpicAsync(Epic create)
        {
            var createdEntity = await _context.Epics.AddAsync(create);
            await _context.SaveChangesAsync();
            return _mapper.Map<EpicConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            _context.Epics.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Epic>> GetAllAsync()
        {
            var items = await _context.Epics.ToListAsync();
            return items;
        }

        public async Task<Epic> GetByIdAsync(Guid id)
        {
            return _context.Epics.FirstOrDefault(e => e.BacklogItemId == id);
        }

        public Task<List<Epic>> GetEpicsByBacklogIdAsync(Guid id)
        {
           
            return _context.Epics.Where(e => e.BacklogId == id).ToListAsync();
            
        }

        public async Task UpdateAsync(Epic update)
        {
            var item = await _context.Epics.FindAsync(update.BacklogItemId);
            if (item != null)
            {
                item.BacklogItemName = update.BacklogItemName;


                await _context.SaveChangesAsync();
            }
        }
    }
}
