using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Models.DTO.AcceptanceCriteriaDTOs
{
    public class AcceptanceCriteriaConfirmationDto
    {
        public string AcceptanceCriteriaText { get; set; }

        public Guid BacklogItemId { get; set; }
        public BacklogItem BacklogItem { get; set; }
    }
}
