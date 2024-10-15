using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.Entities
{
    public class Description
    {
        [Key]
        public Guid DescriptionId { get; set; }
        [Required]
        [MaxLength(200)]
        public string DescriptionText { get; set; }
        public Guid BacklogItemId { get; set; }
        public BacklogItem BacklogItem { get; set; }

       
    }
}
