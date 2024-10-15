using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using User_Service.Models.Enums;

namespace User_Service.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [Required]
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [EnumDataType(typeof(RoleEnum))]
        public RoleEnum Role {  get; set; }
    }
}
