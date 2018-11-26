using Microsoft.EntityFrameworkCore;

namespace ConfigurationDemo.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<ApplicationConfigurationItem> ApplicationConfigurationItem { get; set; }
    }
}
