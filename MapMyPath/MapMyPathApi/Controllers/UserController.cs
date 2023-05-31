using MapMyPathCore.Interfaces;
using MapMyPathCore.Services;
using MapMyPathLib;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MapMyPathApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private IAccountService accountService;
        private IRouteService routeService;

        public UserController(IAccountService accountService, IRouteService routeService)
        {
            this.accountService = accountService;
            this.routeService = routeService;
        }

        [HttpGet]
        [Route("getallusers")]
        public JsonResult GetAllUsers()
        {
            IList<AppUser> usersList = new List<AppUser>();
            try
            {
                usersList = accountService.GetUsers().ToList();
                return new JsonResult(usersList);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }

        [HttpGet]
        [Route("adduser/{username}/{password}/{firstname}/{lastname}")]
        public JsonResult AddUser(string username, string password, string firstName, string lastName)
        {
            if (!accountService.ExistsInDatabase(username))
            {
                try
                {
                    var user = new AppUser
                    {
                        CreatedAt = DateTime.Now,
                        UserName = username,
                        FirstName = firstName,
                        LastName = lastName,
                        Password = password
                    };

                    accountService.AddUser(user);
                    return new JsonResult("Success");
                }
                catch (Exception e)
                {
                    return new JsonResult(e.Message);
                }
            }
            else
            {
                return new JsonResult("There is already a user in database with this email.");
            }
        }

        [HttpGet]
        [Route("validateuser/{username}/{password}")]
        public JsonResult ValidateUser(string username, string password)
        {
            if (accountService.ValidateUser(username, password))
            {
                return new JsonResult("Success");
            }
            else
            {
                return new JsonResult("Error");
            }
        }

        [HttpGet]
        [Route("deleteuser/{username}")]
        public JsonResult DeleteUser(string username)
        {
            if (accountService.ExistsInDatabase(username))
            {
                try
                {
                    var deleted = accountService.DeleteUser(username);
                    return new JsonResult("Success");
                }
                catch (Exception e)
                {
                    return new JsonResult(e.Message);
                }
            }
            else
            {
                return new JsonResult("There is no account in the database with this username");
            }
        }

        [HttpGet]
        [Route("createroute/{username}")]
        public JsonResult CreateRoute(string username)
        {
            try
            {
                string routeId = routeService.CreateRoute(username);
                return new JsonResult(routeId);
            }
            catch (Exception e)
            {
                return new JsonResult("Error");
            }
        }

        [HttpGet]
        [Route("addstoppingpoint/{routeid}/{lat}/{lon}/{order}")]
        public JsonResult AddStoppingPoint(string routeId, double lat, double lon, int order)
        {
            if (routeService.AddStoppingPoint(routeId, lat, lon, order))
            {
                return new JsonResult("Success");
            }
            else
            {
                return new JsonResult("Error");
            }
        }

        [Route("getuserroutes/{username}")]
        [HttpGet]
        public JsonResult GetUserRoutes(string username)
        {
            try
            {
                var routes = routeService.GetRoutesForUser(username);
                return new JsonResult(routes);
            }
            catch (Exception)
            {
                return new JsonResult("Error");
            }
        }

        [HttpGet]
        [Route("getcoordinatesforroute/{routeid}")]
        public JsonResult GetCoordinatesForRoute(string routeId)
        {
            try
            {
                var coordinates = routeService.GetCoordinatesForRoute(routeId);
                return new JsonResult(coordinates);
            }
            catch (Exception)
            {
                return new JsonResult("Error");
            }
        }
    }
}