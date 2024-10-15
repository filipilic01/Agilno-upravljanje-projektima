using User_Service.Models.Enums;

namespace User_Service.Models
{
    public class UserConfirmation
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }

    }
}
