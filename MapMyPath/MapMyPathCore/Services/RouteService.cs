using MapMyPathCore.Data;
using MapMyPathLib;

namespace MapMyPathCore.Services
{
    public class RouteService
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
    }
}
