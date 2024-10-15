using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Status_MicroService.Entities 
{
    public class StatusDBContext : DbContext
    {
        private readonly IConfiguration configuration;

        public StatusDBContext(DbContextOptions<StatusDBContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Status> Status { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("StatusDBConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Status>()
                .HasData(new
                {
                    IdStatusa = Guid.Parse("4E4BBBA9-9DBE-4F8D-847A-310E08D26DE5"),
                    VrednostStatusa = "Done",
                    BacklogItemId = Guid.Parse("4E4BBBA9-9DBE-4F8D-847A-310E08D26DE2")
                });
            builder.Entity<Status>()
                .HasData(new
                {
                    IdStatusa = Guid.Parse("B293A04A-2900-48B4-9B1C-A6F3455AA8D5"),
                    VrednostStatusa = "Done",
                    BacklogItemId = Guid.Parse("4E4BBBA9-9DBE-4F8D-847A-310E08D26DE9")
                });
            
        }
    }
}
