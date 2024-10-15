using User_Service.Models.Enums;

namespace User_Service.Models.User
{
    public class UserUpdateDto
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
