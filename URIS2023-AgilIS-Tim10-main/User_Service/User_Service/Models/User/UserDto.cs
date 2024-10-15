using User_Service.Models.Enums;

namespace User_Service.Models.User
{
    public class UserDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }

    }
}
