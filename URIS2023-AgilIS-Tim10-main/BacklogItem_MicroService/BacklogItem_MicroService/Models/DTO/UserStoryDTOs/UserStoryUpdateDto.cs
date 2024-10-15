namespace BacklogItem_MicroService.Models.DTO.UserStoryDTOs
{
    public class UserStoryUpdateDto
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }
    }
}
