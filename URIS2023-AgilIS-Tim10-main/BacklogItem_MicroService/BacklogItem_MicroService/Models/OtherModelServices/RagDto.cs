namespace BacklogItem_MicroService.Models.OtherModelServices
{
    public class RagDto
    {
        public Guid RagId { get; set; }
        public string RagValue { get; set; }
        
        public Guid BacklogItemId { get; set; }
    }
}
