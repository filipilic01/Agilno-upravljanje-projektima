using System.ComponentModel.DataAnnotations;

namespace Status_MicroService.Model
{
    public class StatusUpdateDto
    {
        [Required(ErrorMessage = "Obavezno je uneti id statusa-a!")]
        public Guid IdStatusa { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti vrednost statusa!")]
        public string VrednostStatusa { get; set; }
       
    }
}
