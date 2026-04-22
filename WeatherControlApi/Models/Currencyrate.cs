using System;
using System.Collections.Generic;

namespace WeatherControlApi.Models;

public partial class Currencyrate
{
    public int Id { get; set; }

    public string? CurrencyCode { get; set; }

    public string? Rate { get; set; }

    public DateTime? Date { get; set; }
}
