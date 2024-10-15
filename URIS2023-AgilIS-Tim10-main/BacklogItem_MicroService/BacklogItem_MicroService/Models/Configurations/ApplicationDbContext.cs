using BacklogItem_MicroService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Contracts;

namespace BacklogItem_MicroService.Models.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        
        public DbSet<Functionality> Functionalities { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<AcceptanceCriteria> AcceptanceCriterias { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<StoryPoint> StoryPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BacklogItem>()
                .HasOne(ac => ac.AcceptanceCriteria)
                .WithOne(b => b.BacklogItem)
                .HasForeignKey<AcceptanceCriteria>(ad => ad.BacklogItemId);

            modelBuilder.Entity<BacklogItem>()
                .HasOne(d => d.Description)
                .WithOne(b => b.BacklogItem)
                .HasForeignKey<Description>(ad => ad.BacklogItemId);

            modelBuilder.Entity<BacklogItem>()
                .HasOne(sp => sp.StoryPoint)
                .WithOne(b => b.BacklogItem)
                .HasForeignKey<StoryPoint>(ad => ad.BacklogItemId);
        }

    }
}
