using MapMyPathWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather;

namespace MapMyPathWebTests
{
    [TestFixture]
    internal class WeatherServiceTests
    {
        private WeatherService service;
        [SetUp]
        public void Setup()
        {
            service = new WeatherService();
        }

        [Test]
        public async Task GetWeatherFromOpenWeatherApi_returnsWeatherCity()
        {
            var city = "Zagreb";
            var result = await service.GetWeatherFromOpenWeatherApi(city);
            Assert.IsInstanceOf<WeatherCity>(result);
            Assert.IsNotNull(result);
        }
    }
}
