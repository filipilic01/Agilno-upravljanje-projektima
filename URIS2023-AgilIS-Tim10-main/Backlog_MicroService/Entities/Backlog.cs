using Backlog_MicroService.Models;
using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Entities
{
    public class Backlog
    {
        [Key]
        [Required]
        public Guid IdBacklog { get; set; }
        public string Opis { get; set; }
        
        [Required]
        public string Naslov { get; set; }

        public Guid? KorisnikId { get; set; }
        public Guid ProjekatId { get; set; }


    }
}
