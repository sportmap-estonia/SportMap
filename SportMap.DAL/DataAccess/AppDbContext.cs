using Microsoft.EntityFrameworkCore;

namespace SportMap.DAL.DataContext
{
    public class AppDbContext : DbContext
    {
        internal AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
