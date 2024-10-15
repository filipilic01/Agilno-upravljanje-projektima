using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Status_MicroService.Entities
{
    [Index(nameof(BacklogItemId), IsUnique = true)]
    public class Status
    {
        [Key]
        [Required]
        public Guid IdStatusa  { get; set; }

        public string VrednostStatusa { get; set; }
        public Guid BacklogItemId { get; set; }
    }
}
