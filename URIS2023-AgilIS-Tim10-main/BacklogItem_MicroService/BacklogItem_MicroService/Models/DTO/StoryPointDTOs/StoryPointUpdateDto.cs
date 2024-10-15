using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.DTO.StoryPointDTOs
{
    public class StoryPointUpdateDto
    {

        public Guid StoryPointId { get; set; }

        public StoryPointEnum StoryPointValue { get; set; }

  
    }
}
