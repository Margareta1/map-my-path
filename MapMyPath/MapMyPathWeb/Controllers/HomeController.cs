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
            var routes = _accountService.GetUserRoutes(User?.Identity?.Name);
            return View(routes);
        }

        [HttpGet]
        public IActionResult GetRoutes()
        {
            string username = User?.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                // return an empty list or an error message if no user is logged in
                return Json(new List<MapMyPathLib.Route>());
            }
            else
            {
                var routes = _accountService.GetUserRoutes(username);

                foreach (var item in routes)
                {
                    _logger.LogInformation(item.IdRoute.ToString());
                }
                return Json(routes);
            }
        }

        [HttpGet]
        public IActionResult GetCoordinatesForRoute(string routeId)
        {
            var coordinates = _accountService.GetCoordinatesForRoute(routeId);
            coordinates = coordinates.OrderBy(c => c.StoppingOrder).ToList();
            return Json(coordinates);
        }

        [HttpPost]
        public async Task<JsonResult> CreateRoute([FromBody] RouteRequest request)
        {
            if (User.Identity.Name == null)
            {
                return Json(new { success = false });
            }
            var _username = User.Identity.Name;
            var simpleCoordinates = request.SimpleCoordinates;

            // _logger.LogInformation(_username);
            await _accountService.CreateRoute(_username);
            var routes = _accountService.GetUserRoutes(_username);

            var routeId = routes.Last().IdRoute;

            List<Coordinate> coordinates = new List<Coordinate>();
            for (int i = 0; i < simpleCoordinates.Count; i++)
            {
                // _logger.LogInformation("Latitude; " + simpleCoordinates[i].Latitude.ToString());
                // _logger.LogInformation("Longitude; " + simpleCoordinates[i].Longitude.ToString());
                Coordinate coordinate = new Coordinate
                {
                    IdCoordinate = Guid.NewGuid(),
                    RouteId = routeId,
                    Latitude = simpleCoordinates[i].Latitude,
                    Longitude = simpleCoordinates[i].Longitude,
                    StoppingOrder = i + 1
                };
                coordinates.Add(coordinate);
                bool added = _accountService.AddStoppingPoint(routeId.ToString(), coordinate.Latitude, coordinate.Longitude, i + 1);

                if (!added)
                {
                    return Json(new { success = false });
                }
            }

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