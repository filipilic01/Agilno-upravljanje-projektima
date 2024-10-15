using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Models.Backlog
{
    public class BacklogUpdateDto
    {
        [Required(ErrorMessage = "Obavezno je uneti id backlog-a!")]
        public Guid IdBacklog { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti opis")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti Naslov")]
        public string Naslov { get; set; }


        public Guid? ProekatId { get; set; }
        public Guid? KorisnikId { get; set; }

    }
}
