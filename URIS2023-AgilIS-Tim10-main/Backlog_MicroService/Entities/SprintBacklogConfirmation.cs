using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Entities
{
    public class SprintBacklogConfirmation : Backlog
    {
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }
        public string Cilj { get; set; }


    }
}
