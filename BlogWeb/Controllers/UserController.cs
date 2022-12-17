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
            var userId = HttpContext.Session.GetInt32("_UserToken");
            var degerler = c.User.FirstOrDefault(x => x.UserId == userId);
            ViewBag.user = degerler;

<<<<<<< HEAD
=======
           // return View(db.Blogs.Where(x => x.UserId == "Sessiondan gelen user id si").ToList());

>>>>>>> a97ad58b51f639e1848a65626a641393b07f9350
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

        public IActionResult Profil(int id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("_UserToken"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;
            
            return View(degerler);
        }
        [HttpPost]
        public IActionResult ProfilEdit(UserModel us)
        {
            var x = user.TGet(us.UserId);
            if (ModelState.IsValid)
            {
                x.FirstName = us.FirstName;
                x.LastName = us.LastName;
                x.Email = us.Email;
                x.Password = us.Password;
                x.RoleId = 1;
                user.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return RedirectToAction("Contact");
            }
            return View();
        }



    }
}
