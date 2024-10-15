using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.DTO.BacklogItemDTOs
{
    public class BacklogItemDto
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }
        
    }
}
