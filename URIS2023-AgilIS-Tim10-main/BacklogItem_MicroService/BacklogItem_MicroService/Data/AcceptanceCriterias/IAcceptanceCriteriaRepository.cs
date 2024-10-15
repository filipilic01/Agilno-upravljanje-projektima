using BacklogItem_MicroService.Data.CrudRepository;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Data.AcceptanceCriterias
{
    public interface IAcceptanceCriteriaRepository: IRepository<AcceptanceCriteria>
    {
        Task<AcceptanceCriteriaConfirmation> CreateAcceptanceCriteriaAsync(AcceptanceCriteria create);
    }
}
