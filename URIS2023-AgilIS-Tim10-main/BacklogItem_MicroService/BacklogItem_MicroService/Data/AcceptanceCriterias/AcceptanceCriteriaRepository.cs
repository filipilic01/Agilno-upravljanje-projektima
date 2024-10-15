using BacklogItem_MicroService.Models.Configurations;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoMapper;

namespace BacklogItem_MicroService.Data.AcceptanceCriterias
{
    public class AcceptanceCriteriaRepository : IAcceptanceCriteriaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AcceptanceCriteriaRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AcceptanceCriteriaConfirmation> CreateAcceptanceCriteriaAsync(AcceptanceCriteria create)
        {
            var createdEntity = await _context.AcceptanceCriterias.AddAsync(create);
            await _context.SaveChangesAsync();
            return _mapper.Map<AcceptanceCriteriaConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var criteria = await GetByIdAsync(id);
            _context.AcceptanceCriterias.Remove(criteria);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AcceptanceCriteria>> GetAllAsync()
        {
           
            var criterias = await _context.AcceptanceCriterias.AsNoTracking().Include(b => b.BacklogItem).ToListAsync();
            
            return criterias;
        
        }

        public async Task<AcceptanceCriteria> GetByIdAsync(Guid id)
        {
            return _context.AcceptanceCriterias.AsNoTracking().Include(b => b.BacklogItem).FirstOrDefault(e => e.AcceptanceCriteriaId == id);
        }

        public async Task UpdateAsync(AcceptanceCriteria update)
        {
            var criteria = await _context.AcceptanceCriterias.FindAsync(update.AcceptanceCriteriaId);
            if (criteria != null)
            {
                criteria.AcceptanceCriteriaText = update.AcceptanceCriteriaText;


                await _context.SaveChangesAsync();
            }
        }
    }
}
