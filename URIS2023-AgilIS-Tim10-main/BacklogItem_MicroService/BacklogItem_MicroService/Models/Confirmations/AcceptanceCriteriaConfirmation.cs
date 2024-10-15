using BacklogItem_MicroService.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BacklogItem_MicroService.Models.Confirmations
{
    public class AcceptanceCriteriaConfirmation
    {
        public Guid AcceptanceCriteriaId { get; set; }
        public string AcceptanceCriteriaText { get; set; }
        public Guid BacklogItemId { get; set; }
        public BacklogItem BacklogItem { get; set; }
    }
}
