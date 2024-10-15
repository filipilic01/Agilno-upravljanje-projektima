using System.ComponentModel.DataAnnotations;

namespace User_Service.Models.OtherModelServices
{
    public class ProjectDTO
    {
        // public string ProjectName { get; set; }

        public string NazivProjekta { get; set; }

        public string OpisProjekta { get; set; }

        public DateTime DatumProjekta { get; set; }

        public string TimID { get; set; }
    }
}
