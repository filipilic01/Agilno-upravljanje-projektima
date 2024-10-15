using AutoMapper;
using BacklogItem_MicroService.Models.Configurations;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace BacklogItem_MicroService.Data.BacklogItems
{
    public class BacklogItemRepository : IBacklogItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BacklogItemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BacklogItemConfirmation> CreateBacklogItemAsync(BacklogItem create)
        {
            
            var createdEntity = await _context.BacklogItems.AddAsync(create);
            await _context.SaveChangesAsync();
            return _mapper.Map<BacklogItemConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
             _context.BacklogItems.Remove(item);
            await  _context.SaveChangesAsync();
        }

        public async  Task<AcceptanceCriteria> GetAcceptanceCriteriaByBacklogItemIdAsync(Guid id)
        {
            return _context.AcceptanceCriterias.FirstOrDefault(e => e.BacklogItemId == id);
        }

        public async Task<List<BacklogItem>> GetAllAsync()
        {
            var items = await _context.BacklogItems.ToListAsync();
            return items;
        }

        public Task<List<BacklogItem>> GetBacklogItemsByBacklogIdAsync(Guid id)
        {
            return _context.BacklogItems.Where(e=> e.BacklogId==id).ToListAsync();
        }

        public async Task<BacklogItem> GetByIdAsync(Guid id)
        {
            return  _context.BacklogItems.FirstOrDefault(e => e.BacklogItemId == id);
        }

        public async Task<Description> GetDescriptionByBacklogItemIdAsync(Guid id)
        {
            return _context.Descriptions.FirstOrDefault(e => e.BacklogItemId == id);
        }

        public async Task<StoryPoint> GetStoryPointByBacklogItemIdAsync(Guid id)
        {
            return _context.StoryPoints.FirstOrDefault(e => e.BacklogItemId == id);
        }

        public async Task UpdateAsync(BacklogItem update)
        {
            var item = await _context.BacklogItems.FindAsync(update.BacklogItemId);
            if(item != null)
            {
                item.BacklogItemName = update.BacklogItemName; 
                item.UserId = update.UserId;
                item.BacklogId = update.BacklogId;
              

                await _context.SaveChangesAsync();
            }
       
        }
    }
}
