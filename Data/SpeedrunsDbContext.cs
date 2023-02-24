using Microsoft.EntityFrameworkCore;
using SpeedrunsAngular.Models;

namespace SpeedrunsAngular.Data
{
    public class SpeedrunsDbContext : DbContext
    {
        public SpeedrunsDbContext(DbContextOptions<SpeedrunsDbContext> options) : base(options)
        {
            
        }
        public DbSet<Users> users { get; set; }
        public DbSet<Games> games { get; set; }
        public DbSet<Speedruns> speedruns { get; set; }
    }
}
