using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Entities
{
    public class ProductBacklog : Backlog
    {
        [Required]
        public string  OpisZadatak { get; set; }

        public string VremeTrajanja { get; set; }

        public string AngazovaniRadnici { get; set; }
    }
}
