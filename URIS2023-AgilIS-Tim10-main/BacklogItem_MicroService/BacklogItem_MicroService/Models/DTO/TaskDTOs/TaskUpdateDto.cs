namespace BacklogItem_MicroService.Models.DTO.TaskDTOs
{
    public class TaskUpdateDto
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }
    }
}
