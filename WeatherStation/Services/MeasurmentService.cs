using WeatherStation.Entities;

namespace WeatherStation.Services
{
    public interface IMeasurmentService
    {
        void SaveMeasure(Measurement measurement);
        List<Measurement> GetMeasurements();
        void DropDatabase();
    }

    public class MeasurmentService : IMeasurmentService
    {
        private readonly CustomDbContext _dbContext;
        private readonly ILogger<MeasurmentService> _logger;

        public MeasurmentService(CustomDbContext dbContext, ILogger<MeasurmentService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<Measurement> GetMeasurements()
        {
            return _dbContext.Measurements.ToList();
        }

        public void SaveMeasure(Measurement measurement)
        {
            if (measurement == null)
            {
                _logger.LogError("Measurment is empty");
                return;
            }
            string timeZoneId = "Central European Standard Time"; // Przykład: Polska
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTime currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

            measurement.TimeStamp = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

            _dbContext.Add(measurement);
            _dbContext.SaveChanges();
        }

        public void DropDatabase()
        {
            var fields = _dbContext.Measurements.ToList();
            _dbContext.Measurements.RemoveRange(fields);
        }
    }
}
