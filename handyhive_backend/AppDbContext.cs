using handyhive_backend.models;
using Microsoft.EntityFrameworkCore;

namespace handyhive_backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<WorkRequest> WorkRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<WorkRequest>()
             .HasKey(w => w.Id);
        }
    }
}
