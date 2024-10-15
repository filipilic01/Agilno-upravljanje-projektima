using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.Entities
{
    public class AcceptanceCriteria
    {
        [Key]
        public Guid AcceptanceCriteriaId { get; set; }
        [Required]
        [MaxLength(200)]
        public string AcceptanceCriteriaText { get; set; }
        
        public Guid BacklogItemId { get; set; }
        public BacklogItem BacklogItem { get; set; }
    }
}
