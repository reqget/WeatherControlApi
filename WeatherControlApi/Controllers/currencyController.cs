using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WeatherControlApi;
using WeatherControlApi.Data;
using WeatherControlApi.Models;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly currencyrates _context;

    public CurrencyController(currencyrates context)
    {
        _context = context;
    }

    [HttpPost("GetCurrencyAndSave")]
    public async Task<IActionResult> GetCurrencyAndSave()
    {
        var url = "https://www.doviz.com/";

        using var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var now = DateTime.Now;
        var resultList = new List<currencyDto>();

        var currencies = new Dictionary<string, string>
        {
            { "USD", "//span[@data-socket-key='USD']" },
            { "EUR", "//span[@data-socket-key='EUR']" },
            { "GAU", "//span[@data-socket-key='gram-altin']" }
        };

        foreach (var kvp in currencies)
        {
            var node = doc.DocumentNode.SelectSingleNode(kvp.Value);
            var rawText = node?.InnerText.Trim() ?? "0";

            Console.WriteLine($"[{kvp.Key}] => {rawText}");

            var dto = new currencyDto
            {
                CurrencyCode = kvp.Key,
                Rate = rawText,
                Date = now
            };

            var entity = new Currencyrate
            {
                CurrencyCode = dto.CurrencyCode,
                Rate = dto.Rate,
                Date = dto.Date,
            };

            _context.Currencyrates.Add(entity);
            resultList.Add(dto);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Veriler başarıyla çekildi ve kaydedildi.",
            data = resultList
        });
    }

    [HttpGet("currency")]
    public async Task<IActionResult> GetCurrencyRates()
    {
        var url = "https://www.doviz.com/";

        using var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var usdNode = doc.DocumentNode.SelectSingleNode("//span[@data-socket-key='USD']");
        var eurNode = doc.DocumentNode.SelectSingleNode("//span[@data-socket-key='EUR']");
        var goldNode = doc.DocumentNode.SelectSingleNode("//span[@data-socket-key='gram-altin']");

        var usd = usdNode?.InnerText.Trim() ?? "bulunamadı";
        var eur = eurNode?.InnerText.Trim() ?? "bulunamadı";
        var gold = goldNode?.InnerText.Trim() ?? "bulunamadı";

        var now = DateTime.Now;

        var result = new
        {
            USD = usd,
            EUR = eur,
            GOLD = gold,
            CollectedAt = now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        return Ok(result);
    }
}
