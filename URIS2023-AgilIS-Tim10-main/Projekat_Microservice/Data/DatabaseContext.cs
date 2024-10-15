using Microsoft.EntityFrameworkCore;
using Projekat_Microservice.Models;
using System.Collections.Generic;

namespace Projekat_Microservice.Data
{

    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {

        }
        public DbSet<Projekat> Projekat { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=ProjekatService;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}

