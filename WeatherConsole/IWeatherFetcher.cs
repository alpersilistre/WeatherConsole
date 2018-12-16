using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherConsole
{
    public interface IWeatherFetcher
    {
        public CurrentWeather GetCurrentWeather(string zipCode);
    }
}
