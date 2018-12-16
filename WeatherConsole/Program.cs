using System;

namespace WeatherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting to fetch current weather data.");

            IWeatherFetcher weatherFetcher = new WeatherFetcher();

            var currentWeather = weatherFetcher.GetCurrentWeather("35590");

            Console.WriteLine($"The temp in {currentWeather.name} is {currentWeather.main.temp}");
        }
    }
}
