using System.ComponentModel.DataAnnotations;

namespace Projekat_Microservice.Models
{
    public class BacklogDto
    {
        public Guid IdBacklog { get; set; }
        public string Opis { get; set; }
        public string Naslov { get; set; }

        public Guid? KorisnikId { get; set; }
        public Guid ProjekatId { get; set; }
    }
}
