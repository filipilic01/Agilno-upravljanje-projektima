using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.DTO.BacklogItemDTOs
{
    public class BacklogItemUpdateDto
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }

        public Guid UserId { get; set; }
        public Guid BacklogId {get; set;}
    

    }
}
