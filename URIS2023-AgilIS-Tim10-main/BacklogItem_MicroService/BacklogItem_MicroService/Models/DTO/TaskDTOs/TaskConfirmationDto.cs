namespace BacklogItem_MicroService.Models.DTO.TaskDTOs
{
    public class TaskConfirmationDto
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }

        public Guid UserId { get; set; }

        public Guid BacklogId { get; set; }
    }
}
