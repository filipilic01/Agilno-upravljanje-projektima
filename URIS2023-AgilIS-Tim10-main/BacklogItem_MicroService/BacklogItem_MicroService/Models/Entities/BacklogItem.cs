using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BacklogItem_MicroService.Models.Entities
{
    public class BacklogItem
    {
        [Key]
        public Guid BacklogItemId { get; set; }
        [Required]
        [MaxLength(700)]
        public string BacklogItemName { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid BacklogId { get; set; }

        [JsonIgnore]
        public AcceptanceCriteria AcceptanceCriteria { get; set; }
        [JsonIgnore]
        public StoryPoint StoryPoint { get; set; }
        [JsonIgnore]
        public Description Description { get; set; }



    }
}
