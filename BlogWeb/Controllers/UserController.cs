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
    //[Authorize(Roles = "User")]
    public class UserController : Controller
    {
        BlogRepository blog = new BlogRepository();

        FirebaseAuthProvider auth;
        Context c = new Context();
        UserRepository user = new UserRepository();

       
        public IActionResult Index()
        {

           // return View(db.Blogs.Where(x => x.UserId == "Sessiondan gelen user id si").ToList());

            return View();
        }
        public IActionResult Blogs()
        {
            //if (HttpContext.Session.GetInt32("_UserToken") != null)
            //{
            //    var userid = HttpContext.Session.GetInt32("_UserToken").Value;
            //    var result = user.TGet(userid);
            //    TempData["username"] = result.FirstName;

            //    return View();
            //    // sayfa açılacak

            //}
            return View();
        }



    }
}
