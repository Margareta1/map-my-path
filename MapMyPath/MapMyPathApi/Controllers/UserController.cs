using MapMyPathCore.Services;
using MapMyPathLib;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MapMyPathApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        private AccountService accountService;
        public UserController()
        {
            accountService = new AccountService();
        }

        [HttpGet]
        [Route("getallusers")]
        public JsonResult GetAllUsers()
        {

            IList<AppUser> usersList = new List<AppUser>();
            try
            {
                usersList = accountService.GetUsers().ToList();
                var output = JsonConvert.SerializeObject(usersList);
                return new JsonResult(JsonConvert.DeserializeObject(output));
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

        }

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


        [Route("validateuser/{username}/{password}")]
        public JsonResult ValidateUser(string username, string password)
        {
            if (accountService.ValidateUser(username, password))
            {
                return new JsonResult("Yes");
            }
            else
            {
                return new JsonResult("No");
            }

        }

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

        //[Route("getuserinfo/{username}")]
        //public JsonResult GetUserInfo(string username)
        //{
        //    var user = accountService.GetUserById()

        //}
    }
}
