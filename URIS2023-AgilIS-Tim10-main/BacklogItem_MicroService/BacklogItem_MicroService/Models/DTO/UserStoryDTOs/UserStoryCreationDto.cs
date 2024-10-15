namespace BacklogItem_MicroService.Models.DTO.UserStoryDTOs
{
    public class UserStoryCreationDto
    {
        public string BacklogItemName { get; set; }

        public Guid UserId { get; set; }

        public Guid BacklogId { get; set; }
    }
}
