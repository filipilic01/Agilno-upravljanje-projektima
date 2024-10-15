namespace BacklogItem_MicroService.Models.DTO.FunctionalityDTOs
{
    public class FunctionalityDto
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }
        public Guid UserId { get; set; }

        public Guid BacklogId { get; set; }
    }
}
