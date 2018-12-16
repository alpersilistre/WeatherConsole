using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConsole
{
    class WeatherFetcher : IWeatherFetcher
    {
        private HttpClient client = new HttpClient();

        public CurrentWeather GetCurrentWeather(string zipCode)
        {
            var json = RunAsync(GetApiKey(), zipCode).GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<CurrentWeather>(json);
        }

        private async Task<string> RunAsync(string key, string zipCode)
        {
            client.BaseAddress = new Uri("http://api.openweathermap.org");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var result = "";

            try
            {
                result = await GetWeatherAsync(key, zipCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        private async Task<string> GetWeatherAsync(string key, string zipCode)
        {
            var result = "";
            string url = $"/data/2.5/weather?q={zipCode}&units=metric&appid={key}";

            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine(responseMessage.ToString());
            }

            return result;
        }

        private string GetApiKey()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<Startup>();

            IConfigurationRoot configuration = builder.Build();
            return configuration["SecretValue:key"];
        }
    }
}
