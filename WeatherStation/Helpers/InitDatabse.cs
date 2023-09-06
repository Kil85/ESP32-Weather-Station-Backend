using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherStation.Entities;

namespace WeatherStation.Helpers
{
    public class InitDatabse
    {
        private readonly CustomDbContext _context;
        private readonly ILogger<InitDatabse> _logger;

        public InitDatabse(CustomDbContext context, ILogger<InitDatabse> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void init()
        {
            if (_context.Database.IsRelational())
            {
                try
                {
                    var migrations = _context.Database.GetPendingMigrations();

                    if (migrations.Any())
                    {
                        _context.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    Environment.Exit(500);
                }
            }
        }
    }
}
