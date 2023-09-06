namespace WeatherStation.Entities
{
    public class Measurement
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public string TimeStamp { get; set; }
    }
}
