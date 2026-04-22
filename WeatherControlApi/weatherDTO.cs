namespace WeatherControlApi
{
    public class WeatherDto
    {
        public string CityName { get; set; }
        public double Temperature { get; set; }
     
        public DateTime LocalTime { get; set; }
    }
}
