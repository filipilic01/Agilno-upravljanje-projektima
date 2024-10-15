using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Rag_MicroService.Models.Entities;

namespace Rag_MicroService.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
        }
        public DbSet<Rag> Rags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rag>().HasData(
                new Rag
                {
                    RagId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f0"),
                    RagValue = "green",
                    BacklogItemId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3")
                }
                );
            modelBuilder.Entity<Rag>().HasData(
                new Rag
                {
                    RagId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f1"),
                    RagValue = "red",
                    BacklogItemId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f4")
                }
                );
            modelBuilder.Entity<Rag>().HasData(
                new Rag
                {
                    RagId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f2"),
                    RagValue = "yellow",
                    BacklogItemId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f5")
                }
                );
        }
    }
}
