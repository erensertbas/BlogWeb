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


            return View();
        }
        public IActionResult Blogs()
        {
        
            var userId = HttpContext.Session.GetInt32("_UserToken");
            var userBlog = blog.TList(x => x.UserId == userId);
            return View(userBlog);

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
