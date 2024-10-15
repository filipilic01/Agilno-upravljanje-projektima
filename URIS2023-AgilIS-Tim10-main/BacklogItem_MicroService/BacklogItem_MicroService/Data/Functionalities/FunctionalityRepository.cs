using AutoMapper;
using BacklogItem_MicroService.Models.Configurations;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogItem_MicroService.Data.Functionalities
{
    public class FunctionalityRepository : IFunctionalityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FunctionalityRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FunctionalityConfirmation> CreateFunctionalityAsync(Functionality create)
        {
            var createdEntity = await _context.Functionalities.AddAsync(create);
            await _context.SaveChangesAsync();
            return _mapper.Map<FunctionalityConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            _context.Functionalities.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Functionality>> GetAllAsync()
        {
            var items = await _context.Functionalities.ToListAsync();
            return items;
        }

        public async Task<Functionality> GetByIdAsync(Guid id)
        {
            return _context.Functionalities.FirstOrDefault(e => e.BacklogItemId == id);
        }

        public Task<List<Functionality>> GetFunctionalitiesByBacklogIdAsync(Guid id)
        {
            return _context.Functionalities.Where(e => e.BacklogId == id).ToListAsync();
        }

        public async Task UpdateAsync(Functionality update)
        {
            var item = await _context.Functionalities.FindAsync(update.BacklogItemId);
            if (item != null)
            {
                item.BacklogItemName = update.BacklogItemName;


                await _context.SaveChangesAsync();
            }
        }
    }
}
