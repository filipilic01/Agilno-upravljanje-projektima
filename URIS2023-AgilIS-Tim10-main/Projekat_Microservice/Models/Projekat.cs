using System.ComponentModel.DataAnnotations;

namespace Projekat_Microservice.Models
{
    public class Projekat //naziv datum opis
    {
        
        [Key]
        [Required]
        public Guid ProjekatID { get; set; }

        [Required]
        public string NazivProjekta { get; set; }

        [Required]
        public string OpisProjekta { get; set; }

        [Required]
        public DateTime DatumProjekta { get; set; }

        [Required]
        public Guid TimID { get; set; }

    }
}
    

