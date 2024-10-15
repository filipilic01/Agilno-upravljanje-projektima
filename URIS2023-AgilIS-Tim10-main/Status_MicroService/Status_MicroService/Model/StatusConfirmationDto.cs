using System.ComponentModel.DataAnnotations;

namespace Status_MicroService.Model
{
    public class StatusConfirmationDto
    {
        [Required(ErrorMessage = "Obavezno je uneti vrednost statusa!")]
        public string VrednostStatusa { get; set; }
    }
}
