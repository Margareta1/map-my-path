using MapMyPathWeb.Models;
using MapMyPathWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MapMyPathWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaxiService _taxiService;
        private AccountService _accountService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _taxiService = new TaxiService();
            _accountService = new AccountService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult LiveFeed()
        {
            return View();
        }
        public IActionResult Discover()
        {
            return View();
        }

        public async Task<IActionResult> WeatherInfo()
        {
            var ws = new WeatherService().GetWeatherFromOpenWeatherApi("Zagreb");
            await ws;
            var weather = ws.Result;
            ViewBag.WeatherInfo = weather;
            return View();
        }

        public IActionResult Routes()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetTaxiFareAjax([FromBody] TaxiFareRequest request)
        {
            double depLat = Math.Round(request.DepLat, 2);
            double depLng = Math.Round(request.DepLng, 2);
            double arrLat = Math.Round(request.ArrLat, 2);
            double arrLng = Math.Round(request.ArrLng, 2);

            var taxiFare = await _taxiService.GetTaxiFareAsync(depLat, depLng, arrLat, arrLng);

            return Json(taxiFare);
        }

        public class TaxiFareRequest
        {
            public double DepLat { get; set; }
            public double DepLng { get; set; }
            public double ArrLat { get; set; }
            public double ArrLng { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}