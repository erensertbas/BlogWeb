using BlogWeb.BL.Repository;
using BlogWeb.DL.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BlogWeb.PL.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        FirebaseAuthProvider auth;
        Context c = new Context();
        UserRepository user = new UserRepository();

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("_UserToken") != null)
            {
                var userid = HttpContext.Session.GetInt32("_UserToken").Value;
                var result = user.TGet(userid);
                TempData["username"] = result.FirstName;

            }
            return View();
        }




    }
}
