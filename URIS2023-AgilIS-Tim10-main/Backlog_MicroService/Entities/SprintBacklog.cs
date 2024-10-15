using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Entities
{
    public class SprintBacklog : Backlog
    {
 
            public DateTime Pocetak { get; set; }
            public DateTime Kraj { get; set; }

            [Required]
            public string Cilj { get; set; }
    
           
    }
}
