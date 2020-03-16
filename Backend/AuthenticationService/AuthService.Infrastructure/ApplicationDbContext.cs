using AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        
        public DbSet<User> Users { get; set; }
    }
}