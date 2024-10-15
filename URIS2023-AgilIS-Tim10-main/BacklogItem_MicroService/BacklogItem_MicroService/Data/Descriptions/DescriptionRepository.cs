using AutoMapper;
using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Configurations;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogItem_MicroService.Data.Descriptions
{
    public class DescriptionRepository: IDescriptionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DescriptionRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DescriptionConfirmation> CreateDescriptionAsync(Description create)
        {
            var createdEntity = await _context.Descriptions.AddAsync(create);
            await _context.SaveChangesAsync();
            return _mapper.Map<DescriptionConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var description = await GetByIdAsync(id);
            _context.Descriptions.Remove(description);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Description>> GetAllAsync()
        {
            var descriptions = await _context.Descriptions.AsNoTracking().Include(b => b.BacklogItem).ToListAsync();

            return descriptions;
        }

        public async Task<Description> GetByIdAsync(Guid id)
        {
            return _context.Descriptions.AsNoTracking().Include(b => b.BacklogItem).FirstOrDefault(e => e.DescriptionId == id);

        }

        public async Task UpdateAsync(Description update)
        {
            var description = await _context.Descriptions.FindAsync(update.DescriptionId);
            if (description != null)
            {
                description.DescriptionText = update.DescriptionText;


                await _context.SaveChangesAsync();
            }
        }
    }
}
