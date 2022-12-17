using BlogWeb.BL.Repository;
using BlogWeb.DL.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
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
        RoleRepository role = new RoleRepository();
        BlogRepository blog = new BlogRepository();


        public IActionResult Index()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("_UserToken"));
            var degerler = user.TGet(userId);
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
<<<<<<< HEAD
            return View();
=======
            var userId = HttpContext.Session.GetInt32("_UserToken");
            var userBlog = blog.TList(x => x.UserId == userId);
            return View(userBlog);
>>>>>>> Dev-ErenSertbas
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
            var roles = role.TGet(degerler.RoleId);
            ViewBag.role = roles;

            return View(degerler);
        }
        [HttpPost]
        public IActionResult ProfilEdit(UserModel us)
        {

            var x = user.TGet(us.UserId);
            int id = us.UserId;
            if (ModelState.IsValid)
            {
                x.FirstName = us.FirstName;
                x.LastName = us.LastName;
                x.Email = us.Email;
                x.Password = us.Password;
                x.RoleId = 2;
                user.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return RedirectToAction("Profil", new { id });
            }
            return View();
        }



    }
}
