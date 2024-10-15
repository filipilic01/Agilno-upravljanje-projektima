using BacklogItem_MicroService.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.Confirmations
{
    public class DescriptionConfirmation
    {
        public Guid DescriptionId { get; set; }
        public string DescriptionText { get; set; }
        public Guid BacklogItemId { get; set; }
        public BacklogItem BacklogItem { get; set; }
    }
}
