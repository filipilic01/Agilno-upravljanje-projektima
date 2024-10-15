using Microsoft.EntityFrameworkCore;

namespace Backlog_MicroService.Entities
{
    public class BacklogDBContext : DbContext
    {
        private readonly IConfiguration configuration;

        public BacklogDBContext(DbContextOptions<BacklogDBContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<Backlog> Backlogs { get; set; }
        //public DbSet<ProductBacklog> ProductBacklogs { get; set; }
        public DbSet<SprintBacklog> SprintBacklogs { get; set; }
        public DbSet<ProductBacklog> ProductBacklogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BacklogDBConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
/*
            // builder.Entity<Backlog>().HasIndex(e => e.IdBacklog).IsUnique();
            builder.Entity<Backlog>().ToTable("Backlogs"); // Postavljanje tabele za Backlog entitet

            builder.Entity<SprintBacklog>().ToTable("SprintBacklogs"); // Postavljanje tabele za SprintBacklog entitet
            builder.Entity<SprintBacklog>().HasBaseType<Backlog>(); // Postavljanje SprintBacklog kao izvedene klase od Backlog

            builder.Entity<ProductBacklog>().ToTable("ProductBacklogs"); // Postavljanje tabele za ProductBacklog entitet
            builder.Entity<ProductBacklog>().HasBaseType<Backlog>();

           /* builder.Entity<Backlog>()
               .HasMany(b => b.SprintBacklogs)
               .WithOne()
               .HasForeignKey(s => s.IdBacklog);
            */

            builder.Entity<SprintBacklog>()
                .HasData(new 
                {
                    IdBacklog = Guid.Parse("4E4BBBA9-9DBE-4F8D-847A-310E08D26DE5"),
                    Opis = "Napraviti micro servis za baclog",
                    Naslov = "Novi micro servis",
                    KorisnikId = Guid.Parse("1C989EE3-13B2-4D3B-ABEB-C4E6343EACE1"),
                    ProjekatId = Guid.Parse("7C259EE3-13B2-4D3B-ABEB-C4E6343EACE1"),
                    Pocetak = DateTime.Now,
                    Kraj = DateTime.Now.AddDays(7),
                    Cilj = "Cilj SprintBackloga"
                });
            builder.Entity<ProductBacklog>()
               .HasData(new
               {
                   IdBacklog = Guid.NewGuid(),
                   Opis = "Opis ProductBackloga",
                   Naslov = "Naslov ProductBackloga",
                   KorisnikId = Guid.NewGuid(),
                   ProjekatId = Guid.Parse("7C259EE3-13B2-4D3B-ABEB-C4E6343EACE1"),
                   OpisZadatak = "Opis zadatka ProductBackloga",
                   VremeTrajanja = "Vreme trajanja ProductBackloga",
                   AngazovaniRadnici = "Angažovani radnici ProductBackloga"

               });


        }

    }
}
