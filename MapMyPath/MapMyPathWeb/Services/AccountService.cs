using MapMyPathLib;

namespace MapMyPathWeb.Services
{
    public class AccountService
    {
        private static string URL = "https://mapmypathweb.azurewebsites.net/";
        private static string GETALLUSERS_ENDPOINT = "getallusers";
        private static string ADDUSER_ENDPOINT = "adduser/{0}/{1}/{2}/{3}";
        private static string VALIDATEUSER_ENDPOINT = "validateuser/{0}/{1}";
        private static string DELETEUSER_ENDPOINT = "deleteuser/{0}";
        private static string CREATEROUTE_ENDPOINT = "createroute/{0}";
        private static string ADDSTOPPINGPOINT_ENDPOINT = "addstoppingpoint/{0}/{1}/{2}/{3}";
        private static string GETCOORDINATESFORROUTE_ENDPOINT = "getcoordinatesforroute/{0}";
        private static string GETROUTESFORUSER_ENDPOINT = "getuserroutes/{0}";
        private HttpClient CLIENT;
        private static HttpRequestMessage httpRequest;
        private static HttpMethod method = HttpMethod.Get;
        private List<AppUser> USERS;
        private bool ADDED_USER;
        private bool VALIDATED_USER;
        private bool DELETED_USER;
        private string ROUTECREATED;
        private bool STOPPINGPOINTADDED;
        private List<Coordinate> COORDINATES;
        private List<MapMyPathLib.Route> USER_ROUTES;

        public AccountService()
        {
        }

        public IList<AppUser> GetAllUsers()
        {
            try
            {
                GetAllUsersAsync().Wait();
                return USERS;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private async Task<List<AppUser>> GetAllUsersAsync()
        {
            CLIENT = new HttpClient();

            httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(URL + GETALLUSERS_ENDPOINT)
            };

            List<AppUser> users;
            using (var httpResponse = await CLIENT.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                users = await httpResponse.Content.ReadFromJsonAsync<List<AppUser>>();
            }
            USERS = users;
            return USERS;
        }

        public bool AddUser(string username, string password, string firstName, string lastName)
        {
            try
            {
                AddUserAsync(username, password, firstName, lastName).Wait();
            }
            catch (Exception)
            {
                ADDED_USER = false;
            }
            return ADDED_USER;
        }

        private async Task<bool> AddUserAsync(string username, string password, string firstName, string lastName)
        {
            CLIENT = new HttpClient();

            httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(URL + string.Format(ADDUSER_ENDPOINT, username, password, firstName, lastName))
            };

            string resp;
            using (var httpResponse = await CLIENT.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                resp = await httpResponse.Content.ReadFromJsonAsync<string>();
            }
            if (resp == "Success")
            {
                ADDED_USER = true;
            }
            else
            {
                ADDED_USER = false;
            }
            return ADDED_USER;
        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                ValidateUserAsync(username, password).Wait();
            }
            catch (Exception)
            {
                VALIDATED_USER = false;
            }
            return VALIDATED_USER;
        }

        private async Task<bool> ValidateUserAsync(string username, string password)
        {
            CLIENT = new HttpClient();

            httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(URL + string.Format(VALIDATEUSER_ENDPOINT, username, password))
            };

            string resp;
            using (var httpResponse = await CLIENT.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                resp = await httpResponse.Content.ReadFromJsonAsync<string>();
            }
            if (resp == "Success")
            {
                VALIDATED_USER = true;
            }
            else
            {
                VALIDATED_USER = false;
            }
            return VALIDATED_USER;
        }

        public bool DeleteUser(string username)
        {
            try
            {
                DeleteUserAsync(username).Wait();
            }
            catch (Exception)
            {
                DELETED_USER = false;
            }
            return DELETED_USER;
        }

        private async Task<bool> DeleteUserAsync(string username)
        {
            CLIENT = new HttpClient();

            httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(URL + string.Format(DELETEUSER_ENDPOINT, username))
            };

            string resp;
            using (var httpResponse = await CLIENT.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                resp = await httpResponse.Content.ReadFromJsonAsync<string>();
            }
            if (resp == "Success")
            {
                DELETED_USER = true;
            }
            else
            {
                DELETED_USER = false;
            }
            return DELETED_USER;
        }

        public string CreateRoute(string username)
        {
            try
            {
                CreateRouteAsync(username);
            }
            catch (Exception e)
            {
                ROUTECREATED = null;
            }
            return ROUTECREATED;
        }

        private async Task<string> CreateRouteAsync(string username)
        {
            CLIENT = new HttpClient();

            httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(URL + string.Format(CREATEROUTE_ENDPOINT, username))
            };

            string resp;
            using (var httpResponse = await CLIENT.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                resp = await httpResponse.Content.ReadFromJsonAsync<string>();
            }
            if (resp == "Error")
            {
                ROUTECREATED = null;
            }
            else
            {
                ROUTECREATED = resp;
            }
            return ROUTECREATED;
        }

        public bool AddStoppingPoint(string routeId, double lat, double lon, int order)
        {
            try
            {
                AddStoppingPointAsync(routeId, lat, lon, order).Wait();
            }
            catch (Exception)
            {
                STOPPINGPOINTADDED = false;
            }
            return STOPPINGPOINTADDED;
        }

        private async Task<bool> AddStoppingPointAsync(string routeId, double lat, double lon, int order)
        {
            CLIENT = new HttpClient();

            httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(URL + string.Format(ADDSTOPPINGPOINT_ENDPOINT, routeId, lat, lon, order))
            };

            string resp;
            using (var httpResponse = await CLIENT.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                resp = await httpResponse.Content.ReadFromJsonAsync<string>();
            }
            if (resp == "Success")
            {
                STOPPINGPOINTADDED = true;
            }
            else
            {
                STOPPINGPOINTADDED = false;
            }
            return STOPPINGPOINTADDED;
        }

        public IList<MapMyPathLib.Route> GetUserRoutes(string username)
        {
            try
            {
                GetUserRoutesAsync(username).Wait();
            }
            catch (Exception)
            {
                USER_ROUTES = null;
            }
            return USER_ROUTES;
        }

        private async Task<List<MapMyPathLib.Route>> GetUserRoutesAsync(string username)
        {
            CLIENT = new HttpClient();

            httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(URL + string.Format(GETROUTESFORUSER_ENDPOINT, username))
            };

            var resp = new List<MapMyPathLib.Route>();
            using (var httpResponse = await CLIENT.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                resp = await httpResponse.Content.ReadFromJsonAsync<List<MapMyPathLib.Route>>();
            }
            if (resp == null)
            {
                USER_ROUTES = null;
            }
            else
            {
                USER_ROUTES = resp;
            }
            return USER_ROUTES;
        }

        public IList<Coordinate> GetCoordinatesForRoute(string routeId)
        {
            try
            {
                GetCoordinatesForRouteAsync(routeId).Wait();
            }
            catch (Exception)
            {
                COORDINATES = null;
            }
            return COORDINATES;
        }

        private async Task<List<Coordinate>> GetCoordinatesForRouteAsync(string routeId)
        {
            CLIENT = new HttpClient();

            httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(URL + string.Format(GETCOORDINATESFORROUTE_ENDPOINT, routeId))
            };

            var resp = new List<MapMyPathLib.Coordinate>();
            using (var httpResponse = await CLIENT.SendAsync(httpRequest))
            {
                httpResponse.EnsureSuccessStatusCode();
                resp = await httpResponse.Content.ReadFromJsonAsync<List<MapMyPathLib.Coordinate>>();
            }
            if (resp == null)
            {
                COORDINATES = null;
            }
            else
            {
                COORDINATES = resp;
            }
            return COORDINATES;
        }
    }
}