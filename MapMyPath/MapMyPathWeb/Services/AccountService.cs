using MapMyPathApi.Services;
using MapMyPathLib;

namespace MapMyPathWeb.Services
{
    public class AccountService
    {
        private LocalService localService;
        public AccountService()
        {
            localService = new LocalService();
        }

        public IList<AppUser> GetAllUsers()
        {

            try
            {
                return localService.GetAllUsers().ToList();

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public bool AddUser(string username, string password, string firstName, string lastName)
        {
            try
            {
                localService.AddUser(username, password, firstName, lastName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ValidateUser(string username, string password)
        {
            return localService.ValidateUser(username, password);

        }

        public bool DeleteUser(string username)
        {
            try
            {
                localService.DeleteUser(username);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string CreateRoute(string username)
        {
            try
            {
                return localService.CreateRoute(username);

            }
            catch (Exception e)
            {
                return "Error";
            }
        }

        public bool AddStoppingPoint(string routeId, double lat, double lon, int order)
        {

            return localService.AddStoppingPoint(routeId, lat, lon, order);

        }

        public IList<MapMyPathLib.Route> GetUserRoutes(string username)
        {
            try
            {
                return localService.GetUserRoutes(username);

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
                return localService.GetCoordinatesForRoute(routeId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
