using MapMyPathLib;
using MapMyPathWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace MapMyPathWeb.Controllers
{
    public class UserController : Controller
    {
        private AccountService accountService;
        public UserController()
        {
            accountService = new AccountService();
        }

        public IActionResult SignIn()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                if (accountService.ValidateUser(appUser.UserName, appUser.Password)) { 

               
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(AppUser appUser )
        {
            if (ModelState.IsValid)
            {
               
                accountService.AddUser(appUser.UserName, appUser.Password, appUser.FirstName, appUser.LastName);
                return RedirectToAction("SignIn");
            }
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
        [HttpPost]
        public IActionResult DeleteUser(AppUser user)
        {
            accountService.DeleteUser(user.UserName);
            return View();
        }
    }
}