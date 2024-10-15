namespace BacklogItem_MicroService.Models.DTO.EpicDTOs
{
    public class EpicUpdateDto
    {
        public Guid BacklogItemId { get; set; }
        public string BacklogItemName { get; set; }
    }
}
