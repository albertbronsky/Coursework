using Coursework.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionVote> QuestionVote { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<AnswerVote> AnswerVote { get; set; }
        
        public DbSet<Category> Category { get; set; }
        public DbSet<Country> Country { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<ActiveUser>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("ActiveUsers");
                    eb.Property(v => v.UserName);
                });
            
            modelBuilder
                .Entity<PopularQuestion>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("PopularQuestions");
                    eb.Property(v => v.QuestionId);
                });
            
            modelBuilder
                .Entity<TopAnswer>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("TopAnswers");
                    eb.Property(v => v.AnswerId);
                });
            
            modelBuilder
                .Entity<TopCountry>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("TopCountries");
                    eb.Property(v => v.Name);
                });
            
            modelBuilder
                .Entity<WeeklyRating>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("WeeklyRatings");
                    eb.Property(v => v.Name);
                });
            
            modelBuilder
                .Entity<QuestionPerHour>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("QuestionPerHours");
                    eb.Property(v => v.Hour);
                });
        }
        public DbSet<ActiveUser> ActiveUsers { get; set; }
        public DbSet<PopularQuestion> PopularQuestions { get; set; }
        public DbSet<TopAnswer> TopAnswers { get; set; }
        public DbSet<TopCountry> TopCountries { get; set; }
        public DbSet<WeeklyRating> WeeklyRatings { get; set; }
        public DbSet<QuestionPerHour> QuestionPerHours { get; set; }
    }
}