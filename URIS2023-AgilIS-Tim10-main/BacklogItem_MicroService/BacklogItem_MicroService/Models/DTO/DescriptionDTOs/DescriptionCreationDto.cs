using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Models.DTO.DescriptionDTOs
{
    public class DescriptionCreationDto
    {
        public string DescriptionText { get; set; }
        public Guid BacklogItemId { get; set; }
        
    }
}
