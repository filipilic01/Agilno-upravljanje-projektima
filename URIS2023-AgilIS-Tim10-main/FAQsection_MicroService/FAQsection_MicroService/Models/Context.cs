using FAQsection_MicroService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FAQsection_MicroService.Models
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

        public DbSet<FAQsection_MicroService.Entities.FAQSection> FAQSections { get; set; }
        public DbSet<FAQsection_MicroService.Entities.Question> Questions { get; set; }
        public DbSet<FAQsection_MicroService.Entities.Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FAQsection_MicroService.Entities.Question>()
                .HasOne(a => a.answer)
                .WithOne(q => q.question)
                .HasForeignKey<FAQsection_MicroService.Entities.Answer>(qa => qa.QuestionId);

            modelBuilder.Entity<FAQsection_MicroService.Entities.FAQSection>()
                .HasMany(q => q.Questions)
                .WithOne(fs => fs.faqSection)
                .HasForeignKey(fk => fk.FAQSectionId);

            modelBuilder.Entity<FAQsection_MicroService.Entities.FAQSection>().HasData(
                new Entities.FAQSection
                {
                    FAQSectionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    numberOfQuestions = 3
                }
                );

            modelBuilder.Entity<FAQsection_MicroService.Entities.FAQSection>().HasData(
                new Entities.FAQSection
                {
                    FAQSectionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c1"),
                    numberOfQuestions = 2
                }
                );

            modelBuilder.Entity<FAQsection_MicroService.Entities.FAQSection>().HasData(
                new Entities.FAQSection
                {
                    FAQSectionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c2"),
                    numberOfQuestions = 1
                }
                );

            modelBuilder.Entity<FAQsection_MicroService.Entities.Question>().HasData(
                new Entities.Question
                {
                    QuestionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c3"),
                    QuestionText = "How does the software support agile project management?",
                    FAQSectionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c2")
                }
                );

            modelBuilder.Entity<FAQsection_MicroService.Entities.Question>().HasData(
                new Entities.Question
                {
                    QuestionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c4"),
                    QuestionText = "Does the software tool support different agile methodologies?",
                    FAQSectionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c1")
                }
                );

            modelBuilder.Entity<FAQsection_MicroService.Entities.Answer>().HasData(
                new Entities.Answer
                {
                    AnswerId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c5"),
                    AnswerText = "The software supports agile project management through features like iteration planning, task tracking, resource allocation, and transparent team communication.",
                    QuestionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c3")
                }
                );
            modelBuilder.Entity<FAQsection_MicroService.Entities.Answer>().HasData(
               new Entities.Answer
               {
                   AnswerId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c6"),
                   AnswerText = "Yes, the software tool is flexible and can accommodate various agile methodologies, allowing users to customize it to their team's needs.",
                   QuestionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c4")

               }
               );

        }

    }
}
