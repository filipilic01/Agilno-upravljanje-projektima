namespace Backlog_MicroService.Models
{
    public class BacklogItemDto
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }
        public Guid UserId { get; set; }
        public Guid BacklogId { get; set; }

    }
}
