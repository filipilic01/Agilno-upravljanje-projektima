using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Models.SprintBacklog
{
    public class SprintBacklogDto
    {
        /// <summary>
        /// Početak izvršavanja sprinta
        /// </summary>
        public DateTime Pocetak { get; set; }

        /// <summary>
        /// Kraj izvršavanja sprinta
        /// </summary>
        public DateTime Kraj { get; set; }

        /// <summary>
        /// Cilj koji želimo da postignemo u sprintu
        /// </summary>
        public string Cilj { get; set; }

        public Guid IdBacklog { get; set; }

        public string Opis { get; set; }

        public string Naslov { get; set; }

        //public Guid? BacklogItemId { get; set; }
        //public Guid? KorisnikId { get; set; }

    }
}
