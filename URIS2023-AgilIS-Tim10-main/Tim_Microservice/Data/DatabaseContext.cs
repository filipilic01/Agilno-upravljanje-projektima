using Microsoft.EntityFrameworkCore;
using Tim_Microservice.Models;

namespace Tim_Microservice.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {
        }
        public DbSet<Tim> Tim { get; set; }
        public DbSet<ClanTima> ClanTima { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=TimService;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClanTima>()
         .HasOne(clan => clan.Tim)
         .WithMany()
         .HasForeignKey(clan => clan.TimId)
         .OnDelete(DeleteBehavior.Cascade);
        }
    }
}