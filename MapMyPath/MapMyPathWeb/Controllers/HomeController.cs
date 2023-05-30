using MapMyPathLib;
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
        public JsonResult CreateRoute([FromBody] RouteRequest request)
        {
            if (User.Identity.Name != null)
            {
                var username = User.Identity.Name;
            }
            var simpleCoordinates = request.SimpleCoordinates;

            _accountService.CreateRoute("jane.d@gmail.com");
            var routes = _accountService.GetUserRoutes("jane.d@gmail.com");
            var routeId = routes.Last().IdRoute;

            List<Coordinate> coordinates = new List<Coordinate>();
            for (int i = 0; i < simpleCoordinates.Count; i++)
            {
                Coordinate coordinate = new Coordinate
                {
                    IdCoordinate = Guid.NewGuid(),
                    RouteId = routeId,
                    Latitude = simpleCoordinates[i].Latitude,
                    Longitude = simpleCoordinates[i].Longitude,
                    StoppingOrder = i + 1
                };
                coordinates.Add(coordinate);
                _logger.LogInformation("Start");
                _logger.LogInformation("Id" + coordinate.IdCoordinate + "\n");
                _logger.LogInformation("RouteId" + coordinate.RouteId + "\n");
                _logger.LogInformation("Latitude" + coordinate.Latitude + "\n");
                _logger.LogInformation("Longitude" + coordinate.Longitude + "\n");
                _logger.LogInformation("StoppingOrder" + coordinate.StoppingOrder + "\n");
                _logger.LogInformation("End");
                bool added = _accountService.AddStoppingPoint(routeId.ToString(), coordinate.Latitude, coordinate.Longitude, i + 1);

                if (!added)
                {
                    // Handle error: adding stopping point failed
                    return Json(new { success = false });
                }
            }

            // If everything is successful, return the routeId in a JSON object
            return Json(new { success = true, routeId = routeId });
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

        public class SimpleCoordinate
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public class RouteRequest
        {
            public List<SimpleCoordinate> SimpleCoordinates { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}