using MapMyPathLib;
using Newtonsoft.Json;
using Weather;

namespace MapMyPathCore.Services
{
    public class WeatherService
    {
        public async Task<WeatherCity> GetWeatherFromOpenWeatherApi(string city)
        {
            string apiKey = "cda5d4bb347f5cd215128a802a0419b6";
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + apiKey;

            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync(url);
            var weatherData = JsonConvert.DeserializeObject<WeatherCity>(response);

            return weatherData;
        }
    }
}