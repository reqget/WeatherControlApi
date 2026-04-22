using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherControlApi;
using WeatherControlApi.Data;
using WeatherControlApi.Models;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly WeatherDbContext _context;

    public WeatherController(WeatherDbContext context, HttpClient httpClient, IConfiguration config)
    {
        _context = context;
        _httpClient = httpClient;
        _config = config;
    }

    [HttpPost("GetWeatherAndSave")]
    public async Task<IActionResult> GetWeatherAndSave(string city)
    {
        var apiKey = _config["OpenWeather:ApiKey"];
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&lang=tr&appid={apiKey}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, "Hava durumu alınamadı.");

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var weatherDto = new WeatherDto
        {
            CityName = root.GetProperty("name").GetString(),
            Temperature = root.GetProperty("main").GetProperty("temp").GetDouble()
        };

        var weather = new Cityweather
        {
            CityName = weatherDto.CityName,
            CityDegree = weatherDto.Temperature,
            Time = DateTime.Now
        };

        _context.Cityweathers.Add(weather);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Hava durumu başarıyla çekildi ve kaydedildi.",
            data = weather
        });
    }
}
