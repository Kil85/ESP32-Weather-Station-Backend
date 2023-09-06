using Microsoft.EntityFrameworkCore;

namespace WeatherStation.Entities
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options)
            : base(options) { }

        public DbSet<Measurement> Measurements { get; set; }
    }
}
