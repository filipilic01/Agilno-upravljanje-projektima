using System.ComponentModel.DataAnnotations;

namespace Status_MicroService.Model
{
    public class StatusCreationDto
    {
        [Required(ErrorMessage = "Obavezno je uneti vrednost statusa!")]
        public string VrednostStatusa { get; set; }

        public Guid BacklogItemId { get; set; }
    }
}
