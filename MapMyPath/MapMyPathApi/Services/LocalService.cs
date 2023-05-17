using MapMyPathApi.Controllers;
using MapMyPathCore.Services;
using MapMyPathLib;

namespace MapMyPathApi.Services
{
    public class LocalService
    {
        private RouteService routeService;
        private AccountService accountService;
        public LocalService()
        {
            routeService = new RouteService();
            accountService = new AccountService();
        }

        public IList<AppUser> GetAllUsers()
        {

            try
            {
                return accountService.GetUsers().ToList();
                
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public bool AddUser(string username, string password, string firstName, string lastName)
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
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public bool ValidateUser(string username, string password)
        {
            return accountService.ValidateUser(username, password);

        }

        public bool DeleteUser(string username)
        {
            if (accountService.ExistsInDatabase(username))
            {
                try
                {
                    var deleted = accountService.DeleteUser(username);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public string CreateRoute(string username)
        {
            try
            {
                return routeService.CreateRoute(username);
                
            }
            catch (Exception e)
            {
                return "Error";
            }
        }

        public bool AddStoppingPoint(string routeId, double lat, double lon, int order)
        {

            return routeService.AddStoppingPoint(routeId, lat, lon, order);

        }

        public IList<MapMyPathLib.Route> GetUserRoutes(string username)
        {
            try
            {
                return routeService.GetRoutesForUser(username);
                
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IList<Coordinate> GetCoordinatesForRoute(string routeId)
        {
            try
            {
                return routeService.GetCoordinatesForRoute(routeId);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
