using User_Service.Entities;
using User_Service.Models;

namespace User_Service.Data
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        public User GetUserById(Guid Id);
        public void DeleteUser(Guid Id);
        public UserConfirmation AddUser(User user);
        public User UpdateUser(User user);
        public bool SaveChanges();
        public User GetUserByUserName(string userName);

    }
}
