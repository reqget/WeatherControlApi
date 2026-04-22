using System;
using System.Collections.Generic;

namespace WeatherControlApi.Models;

public partial class Cityweather
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public DateTime? Time { get; set; }

    public double? CityDegree { get; set; }
}
