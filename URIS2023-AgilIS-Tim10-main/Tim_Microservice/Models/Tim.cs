using System.ComponentModel.DataAnnotations;

namespace Tim_Microservice.Models
{
    public class Tim
    {
        [Key]
        [Required]
        public Guid TimID { get; set; }

        [Required]
        public string NazivTima {  get; set; }

        [Required]
        public string BrojClanova { get; set; }

        [Required]
        public Guid UserId { get; set; }


    }
}
