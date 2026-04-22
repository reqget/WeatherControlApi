using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;
using WeatherControlApi.Models;

namespace WeatherControlApi
{
    public class WeatherService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WeatherService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<weather?> GetSimplifiedWeatherAsync(string city)
        {


            var apiKey = _config["OpenWeather:ApiKey"];
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&lang=tr&appid={apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<WeatherResponse>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null) return null;

            return new weather
            {
                CityName = result.Location.Name,
                Temperature = result.Current.Temp_c,
                Humidity = result.Current.Humidity,
                Description = result.Current.Condition.Text,
                LocalTime = result.Location.Localtime
            };
        }

    }
}
