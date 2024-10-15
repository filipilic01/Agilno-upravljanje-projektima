using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.DTO.StoryPointDTOs
{
    public class StoryPointCreationDto
    {
        public StoryPointEnum StoryPointValue { get; set; }

        public Guid BacklogItemId { get; set; }
   
    }
}
