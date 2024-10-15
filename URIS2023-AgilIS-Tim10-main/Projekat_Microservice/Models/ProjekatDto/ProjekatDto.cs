using System.ComponentModel.DataAnnotations;

namespace Projekat_Microservice.Models.ProjekatDto
{
    public class ProjekatDto
    {
        public Guid ProjekatID { get; set; }

       
        public string NazivProjekta { get; set; }

    
        public string OpisProjekta { get; set; }

    
        public DateTime DatumProjekta { get; set; }
    }
}
