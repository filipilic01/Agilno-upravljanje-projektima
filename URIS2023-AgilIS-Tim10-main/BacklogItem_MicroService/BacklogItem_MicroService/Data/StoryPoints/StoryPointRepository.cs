using AutoMapper;
using BacklogItem_MicroService.Models.Configurations;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogItem_MicroService.Data.StoryPoints
{
    public class StoryPointRepository : IStoryPointRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StoryPointRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StoryPointConfirmation> CreateStoryPointAsync(StoryPoint create)
        {
            var createdEntity = await _context.StoryPoints.AddAsync(create);
            await _context.SaveChangesAsync();
            return _mapper.Map<StoryPointConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var point = await GetByIdAsync(id);
            _context.StoryPoints.Remove(point);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StoryPoint>> GetAllAsync()
        {
            var points = await _context.StoryPoints.AsNoTracking().Include(b => b.BacklogItem).ToListAsync();

            return points;
        }

        public async Task<StoryPoint> GetByIdAsync(Guid id)
        {
            return _context.StoryPoints.AsNoTracking().Include(b => b.BacklogItem).FirstOrDefault(e => e.StoryPointId == id);

        }

        public async Task UpdateAsync(StoryPoint update)
        {
            var point = await _context.StoryPoints.FindAsync(update.StoryPointId);
            if (point != null)
            {
                point.StoryPointValue = update.StoryPointValue;


                await _context.SaveChangesAsync();
            }
        }
    }
}
