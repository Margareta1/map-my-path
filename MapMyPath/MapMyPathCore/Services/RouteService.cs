using MapMyPathCore.Data;
using MapMyPathCore.Interfaces;
using MapMyPathLib;

namespace MapMyPathCore.Services
{
    public class RouteService : IRouteService
    {
        private MapMyPathCoreContext CONTEXT;

        public RouteService()
        {
            CONTEXT = new MapMyPathCoreContext(new Microsoft.EntityFrameworkCore.DbContextOptions<MapMyPathCoreContext>());
        }

        public string CreateRoute(string username)
        {
            var userId = Guid.Parse(CONTEXT.Users.FirstOrDefault(x => x.UserName == username).Id);
            var route = new MapMyPathLib.Route();
            route.UserId = userId;
            CONTEXT.Route.Add(route);
            CONTEXT.SaveChanges();
            var routeId = CONTEXT.Route.ToList().FindLast(x => x.UserId == userId).IdRoute.ToString();
            return routeId;
        }

        public bool AddStoppingPoint(string routeId, double lat, double lon, int order)
        {
            try
            {
                var coordinate = new Coordinate();
                coordinate.Latitude = lat;
                coordinate.Longitude = lon;
                coordinate.RouteId = Guid.Parse(routeId);
                coordinate.StoppingOrder = order;
                CONTEXT.Coordinate.Add(coordinate);
                CONTEXT.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<MapMyPathLib.Route> GetRoutesForUser(string username)
        {
            try
            {
                var userId = Guid.Parse(CONTEXT.Users.FirstOrDefault(x => x.UserName == username).Id);
                return CONTEXT.Route.Where(x => x.UserId == userId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Coordinate> GetCoordinatesForRoute(string routeId)
        {
            try
            {
                return CONTEXT.Coordinate.Where(x => x.RouteId == Guid.Parse(routeId)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}