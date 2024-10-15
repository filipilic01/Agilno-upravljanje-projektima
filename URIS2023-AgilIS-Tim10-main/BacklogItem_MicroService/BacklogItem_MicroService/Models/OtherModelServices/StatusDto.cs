namespace BacklogItem_MicroService.Models.OtherModelServices
{
    public class StatusDto
    {
        public Guid IdStatusa { get; set; }
        public string VrednostStatusa { get; set; }

        public Guid BacklogItemId { get; set; }
    }
}
