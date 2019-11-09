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
    }
}