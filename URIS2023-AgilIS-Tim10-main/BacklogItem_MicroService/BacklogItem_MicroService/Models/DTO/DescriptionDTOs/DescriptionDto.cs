using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Models.DTO.DescriptionDTOs
{
    public class DescriptionDto
    {
        public Guid DescriptionId { get; set; }
        public string DescriptionText { get; set; }
        public Guid BacklogItemId { get; set; }
        public BacklogItem BacklogItem { get; set; }
    }
}
