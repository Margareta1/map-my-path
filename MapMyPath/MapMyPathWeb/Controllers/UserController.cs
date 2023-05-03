using Microsoft.AspNetCore.Mvc;

namespace MapMyPathWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult EditUser()
        {
            return View();
        }

        public IActionResult DeleteUser()
        {
            return View();
        }
    }
}