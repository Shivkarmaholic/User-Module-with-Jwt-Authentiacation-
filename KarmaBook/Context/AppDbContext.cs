using KarmaBook.Models;
using Microsoft.EntityFrameworkCore;

namespace KarmaBook.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
                entity =>
                {
                    entity.HasKey(u => u.UserId);
                    entity.Property(u => u.Roles)
                    .HasConversion<int>();
                }
                
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
