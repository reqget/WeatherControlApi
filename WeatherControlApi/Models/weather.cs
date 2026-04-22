namespace WeatherControlApi.Models
{
    public class weather
    {
        public string CityName { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public string Description { get; set; }
        public DateTime LocalTime { get; set; }
    }
}
