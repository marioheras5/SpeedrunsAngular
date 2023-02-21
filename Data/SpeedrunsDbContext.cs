using Microsoft.EntityFrameworkCore;
using SpeedrunsAngular.Models;

namespace SpeedrunsAngular.Data
{
    public class SpeedrunsDbContext : DbContext
    {
        public SpeedrunsDbContext(DbContextOptions<SpeedrunsDbContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Users> users { get; set; }
    }
}
