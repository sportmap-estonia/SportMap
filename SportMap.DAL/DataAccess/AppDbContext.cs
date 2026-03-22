using DomainLayer.Common;
using Microsoft.EntityFrameworkCore;

namespace SportMap.DAL.DataContext
{
    public class AppDbContext : DbContext
    {
        internal AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<PrivacyType> PrivacyTypes => Set<PrivacyType>();
        public DbSet<Personalization> Personalization => Set<Personalization>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(user => user.GoogleId).IsUnique();
                entity.HasIndex(user => user.Email).IsUnique();
                entity.HasOne(user => user.UserRole)
                      .WithMany()
                      .HasForeignKey(user => user.UserRoleId)
                      .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(user => user.Personalization)
                      .WithOne(personalization => personalization.User)
                      .HasForeignKey<Personalization>(personalization => personalization.UserId);
            });

            modelBuilder.Entity<Personalization>(entity =>
            {
                entity.HasOne(personalization => personalization.BirthdatePrivacyType)
                      .WithMany()
                      .HasForeignKey(personalization => personalization.BirthdatePrivacyId);
            });
        }
    }
}
