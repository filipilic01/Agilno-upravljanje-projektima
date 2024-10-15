namespace User_Service.Models.OtherModelServices
{
    public class BacklogItemDTO
    {
        public string BacklogItemName { get; set; }
        
        public Guid UserId { get; set; }

        public Guid BacklogId { get; set; }
    }
}
