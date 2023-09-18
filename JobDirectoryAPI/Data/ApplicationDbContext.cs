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
    }
}
