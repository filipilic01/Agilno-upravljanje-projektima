using AutoMapper;
using Microsoft.EntityFrameworkCore;
using User_Service.Entities;
using User_Service.Models;

namespace User_Service.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public UserRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void DeleteUser(Guid Id)
        {
            var deletedUser = GetUserById(Id);
            context.Remove(deletedUser);
            context.SaveChanges();
        }

        public User GetUserById(Guid Id)
        {
            return context.Users.FirstOrDefault(e => e.UserId == Id);
        }

        public List<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public UserConfirmation AddUser(User user)
        {
            var createdUser = context.Add(user);
            context.SaveChanges();
            return mapper.Map<UserConfirmation>(createdUser.Entity);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public User UpdateUser(User user)
        {
            try
            {
                var existingUser = context.Users.FirstOrDefault(e => e.UserId == user.UserId);

                if (existingUser != null)
                {

                    existingUser.Name = user.Name;
                    existingUser.Surname = user.Surname;
                    existingUser.Username = user.Username;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;

                    context.SaveChanges(); 

                    return existingUser;
                }
                else
                {
                    throw new KeyNotFoundException($"User not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public User GetUserByUserName(string userName)
        {
            return context.Users.FirstOrDefault(u => u.Username == userName);
        }



    }
}
