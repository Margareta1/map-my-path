using MapMyPathLib;
using MapMyPathWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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
            ClaimsPrincipal userPrincipal = HttpContext.User;
            if (userPrincipal.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(AppUser appUser)
        {
         
                if (accountService.ValidateUser(appUser.UserName, appUser.Password)) { 
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, appUser.UserName),
                        new Claim("OtherProperites", "ExampleRole")
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                    ViewData["Username"] = appUser.UserName;
                    return RedirectToAction("Index", "Home");
                }
            ViewData["ValidateMessage"] = "user not found";

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
        [Authorize]
        public IActionResult EditUser()
        {
            return View();
        }

        [Authorize]
        public IActionResult DeleteUser()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult DeleteUser(AppUser user)
        {
            accountService.DeleteUser(user.UserName);
            return RedirectToAction("SignIn", "User"); 
        }
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "User");
        }
    }
}