using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Models.ProductBacklog
{
    public class ProductBacklogCreationDto
    {
        [Required(ErrorMessage = "Obavezno je uneti Opis")]
        public string OpisZadatak { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti vreme trajanja")]
        public string VremeTrajanja { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti ucesnike u projektu")]
        public string AngazovaniRadnici { get; set; }

       

        [Required(ErrorMessage = "Obavezno je uneti opis backloga")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti Naslov")]
        public string Naslov { get; set; }

        public Guid? ProjekatId { get; set; }
        public Guid? KorisnikId { get; set; }
    }
}
