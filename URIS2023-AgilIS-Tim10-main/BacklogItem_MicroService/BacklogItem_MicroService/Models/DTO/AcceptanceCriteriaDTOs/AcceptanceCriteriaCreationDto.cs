using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Models.DTO.AcceptanceCriteriaDTOs
{
    public class AcceptanceCriteriaCreationDto
    {
        public string AcceptanceCriteriaText { get; set; }

        public Guid BacklogItemId { get; set; }
        
    }
}
