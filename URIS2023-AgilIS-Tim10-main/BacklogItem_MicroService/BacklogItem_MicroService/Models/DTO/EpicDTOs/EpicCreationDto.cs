namespace BacklogItem_MicroService.Models.DTO.EpicDTOs
{
    public class EpicCreationDto
    {
        public string BacklogItemName { get; set; }

        public Guid UserId { get; set; }

        public Guid BacklogId { get; set; }
    }
}
