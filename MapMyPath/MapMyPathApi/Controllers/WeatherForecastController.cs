using MapMyPathCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace MapMyPathApi.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private AccountService accountService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            accountService = new AccountService();

        }



        [Route("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //[Margareta] testing on 02042023
            //var isAddedUser = accountService.AddUser(new MapMyPathLib.AppUser
            //{
            //    FirstName = "Margareta",
            //    LastName = "Zeko",
            //    UserName = "margareta.zeko@gmail.com",
            //    CreatedAt = DateTime.Now,
            //    IsDeleted = 0,
            //    Password = "RandomPassword8888"

            //});
            //var users = accountService.GetUsers();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}