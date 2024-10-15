using BacklogItem_MicroService.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BacklogItem_MicroService.Models.Entities
{
    public class StoryPoint
    {
        [Key]
        public Guid StoryPointId { get; set; }

        [Required]
        [EnumDataType(typeof(StoryPointEnum))]
        public StoryPointEnum StoryPointValue { get; set; }

        public Guid BacklogItemId { get; set; }
        public BacklogItem BacklogItem { get; set; }
    }
}
