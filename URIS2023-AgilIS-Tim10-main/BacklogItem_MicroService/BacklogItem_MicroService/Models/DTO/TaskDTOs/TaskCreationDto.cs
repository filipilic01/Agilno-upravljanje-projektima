namespace BacklogItem_MicroService.Models.DTO.TaskDTOs
{
    public class TaskCreationDto
    {
        public string BacklogItemName { get; set; }

        public Guid UserId { get; set; }

        public Guid BacklogId { get; set; }
    }
}
