using MapMyPathLib;

namespace MapMyPathCore.Interfaces
{
    public interface IRouteService
    {
        string CreateRoute(string username);

        bool AddStoppingPoint(string routeId, double lat, double lon, int order);

        List<MapMyPathLib.Route> GetRoutesForUser(string username);

        List<Coordinate> GetCoordinatesForRoute(string routeId);
    }
}