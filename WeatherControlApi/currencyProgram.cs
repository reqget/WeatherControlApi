using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

class currencyProgram
{
    //static async Task Main()
    //{
    //    var url = "https://www.doviz.com/";

    //    using var httpClient = new HttpClient();
    //    var html = await httpClient.GetStringAsync(url);

    //    var doc = new HtmlDocument();
    //    doc.LoadHtml(html);

    //    // USD/TRY kuru span elementinde data-socket-key="USD" olarak geçiyor
    //    var usdNode = doc.DocumentNode.SelectSingleNode("//span[@data-socket-key='USD']");

    //    if (usdNode != null)
    //    {
    //        var usdRate = usdNode.InnerText.Trim();
    //        Console.WriteLine("USD/TRY Kuru: " + usdRate);
    //    }
    //    else
    //    {
    //        Console.WriteLine("Kur bulunamadı.");
    //    }
    //}
}
