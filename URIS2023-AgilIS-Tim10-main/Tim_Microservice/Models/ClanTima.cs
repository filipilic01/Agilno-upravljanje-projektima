using System.ComponentModel.DataAnnotations;

namespace Tim_Microservice.Models
{
    public class ClanTima
    {
        [Key]
        [Required]
        public Guid ClanTimaId { get; set; }

        public string userName { get; set; }

        public Guid TimId { get; set; }
        public Tim Tim { get; set; }
    }
}
