namespace BacklogItem_MicroService.Models.Confirmations
{
    public class TaskConfirmation
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }
        public Guid UserId { get; set; }

        public Guid BacklogId { get; set; }
    }
}
