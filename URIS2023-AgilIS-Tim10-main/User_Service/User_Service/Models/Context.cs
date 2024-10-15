using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;
using User_Service.Entities;
using User_Service.Models.Enums;

namespace User_Service.Models
{
    public class Context : DbContext
    {
        private readonly IConfiguration configuration;

        public Context(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
        }

        public DbSet<User_Service.Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User_Service.Entities.User>().ToTable("Users");
            builder.Entity<User_Service.Entities.User>().ToTable("Users");
            builder.Entity<User_Service.Entities.User>()
            .HasCheckConstraint("CK_User_Role", "Role IN (0, 1, 2, 3, 4)");


            builder.Entity<User_Service.Entities.User>()
                .HasData(new
                {
                    UserId =Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f0"),
                    Name = "Ana",
                    Surname = "Stanic",
                    Username = "anaS",
                    Email = "anaStanic@gmail.com",
                    Password = "password1",
                    Role = RoleEnum.Developer
                });

            builder.Entity<User_Service.Entities.User>()
               .HasData(new
               {
                   UserId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f1"),
                   Name = "Petar",
                   Surname = "Pap",
                   Username = "petarP",
                   Email = "petarP@gmail.com",
                   Password = "password2",
                   Role = RoleEnum.ProductOwner

               });

            builder.Entity<User_Service.Entities.User>()
               .HasData(new
               {
                   UserId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f2"),
                   Name = "Admin",
                   Surname = "Adnmin",
                   Username = "admin",
                   Email = "admin@gmail.com",
                   Password = "password3",
                   Role = RoleEnum.Admin

               });

            builder.Entity<User_Service.Entities.User>()
             .HasData(new
             {
                 UserId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                 Name = "Milica",
                 Surname = "Kokic",
                 Username = "milicaK",
                 Email = "milicaK@gmail.com",
                 Password = "password4",
                 Role = RoleEnum.Admin

             });

        }

    }

}
