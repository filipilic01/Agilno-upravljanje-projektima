using AutoMapper;
using BacklogItem_MicroService.Models.Configurations;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogItem_MicroService.Data.UserStories
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserStoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserStoryConfirmation> CreateUserStoryAsync(UserStory create)
        {
            var createdEntity = await _context.UserStories.AddAsync(create);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserStoryConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            _context.UserStories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserStory>> GetAllAsync()
        {
            var items = await _context.UserStories.ToListAsync();
            return items;
        }

        public async Task<UserStory> GetByIdAsync(Guid id)
        {
            return _context.UserStories.FirstOrDefault(e => e.BacklogItemId == id);
        }

        public Task<List<UserStory>> GetUserStoriesByBacklogIdAsync(Guid id)
        {
            return _context.UserStories.Where(e => e.BacklogId == id).ToListAsync();
        }

        public async Task UpdateAsync(UserStory update)
        {
            var item = await _context.UserStories.FindAsync(update.BacklogItemId);
            if (item != null)
            {
                item.BacklogItemName = update.BacklogItemName;


                await _context.SaveChangesAsync();
            }
        }
    }
}
