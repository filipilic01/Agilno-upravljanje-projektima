﻿using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Models.SprintBacklog
{
    public class SprintBacklogUpdateDto
    {
        [Required(ErrorMessage = "Obavezno je uneti početak")]
        public DateTime Pocetak { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti kraj")]
        public DateTime Kraj { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti cilj")]
        public string Cilj { get; set; }
        
        [Required(ErrorMessage = "Obavezno je uneti id backlog-a!")]
        public Guid IdBacklog { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti opis")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti naslov")]
        public string Naslov { get; set; }

        public Guid? ProjekatId { get; set; }
        public Guid? KorisnikId { get; set; }
    }
}
