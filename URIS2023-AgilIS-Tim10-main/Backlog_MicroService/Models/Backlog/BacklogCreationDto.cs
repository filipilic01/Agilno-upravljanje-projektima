using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Models.Backlog
{
    public class BacklogCreationDto
    {

        [Required(ErrorMessage = "Obavezno je uneti opis zadataka u backlog-u!")]
        public string Opis { get; set; }
    }
}
