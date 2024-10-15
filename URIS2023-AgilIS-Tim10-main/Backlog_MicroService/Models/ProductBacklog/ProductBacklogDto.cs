using System.ComponentModel.DataAnnotations;

namespace Backlog_MicroService.Models.ProductBacklog
{
    public class ProductBacklogDto
    {
        public Guid IdBacklog { get; set; }
        public string OpisZadatak { get; set; }

        public string VremeTrajanja { get; set; }

        public string AngazovaniRadnici { get; set; }


        public string Opis { get; set; }

        public string Naslov { get; set; }

       
        //public Guid? KorisnikId { get; set; }
    }
}
