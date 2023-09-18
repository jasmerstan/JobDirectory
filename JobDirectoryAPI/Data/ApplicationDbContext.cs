using JobDirectoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobDirectoryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<JobDirectory> JobDirectories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobDirectory>().HasData(
                new JobDirectory()
                {
                    Id = 1,
                    UserName = "Tan Jian Yee",
                    Email = "jasmers47@gmail.com",
                    PhoneNumber = 1234567890,
                    SkillSets = "Skill 1",
                    Hobby = "Skill 2"
                });
        }
    }
}
