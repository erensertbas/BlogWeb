using BlogWeb.BL.Repository;
using BlogWeb.DL.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace BlogWeb.PL.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {

        FirebaseAuthProvider auth;
        Context c = new Context();
        UserRepository user = new UserRepository();
        RoleRepository role = new RoleRepository();
        BlogRepository blog = new BlogRepository();
        CategoryRepository category = new CategoryRepository();

        public string GetCookie(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string id);
            return id;
        }

        public IActionResult Index()
        {
            //int userId = Convert.ToInt32(HttpContext.Session.GetInt32("_UserToken"));

            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;
            ViewBag.userId = userId;

            var userBlog = blog.TList(x => x.UserId == userId);
            ViewBag.userBlogCount = userBlog.Count();
            return View(userBlog);
        }
        public IActionResult Blogs()
        {

            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;
            ViewBag.userId = userId;

            var userBlog = blog.TList(x => x.UserId == userId);
            return View(userBlog);

        }

        public IActionResult BlogCreate()
        {

            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;
            ViewBag.userId = userId;


            List<SelectListItem> values = (from x in c.Category.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString(),
                                           }).ToList();
            ViewBag.v = values;
            return View();
        }

        [HttpPost]
        public IActionResult BlogCreate(BlogEkle b)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;
            ViewBag.userId = userId;


            Blog bl = new Blog();
            bl.UserId = userId;
            if (ModelState.IsValid)
            {
                if (b.ImageUrl != null)
                {
                    var extension = Path.GetExtension(b.ImageUrl.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    b.ImageUrl.CopyTo(stream);
                    bl.ImageUrl = newImageName;
                }
                bl.BlogTitle = b.BlogTitle;
                bl.Text = b.Text;
                bl.Status = false;
                bl.Date = b.Date;

                bl.CategoryId = b.CategoryId;

                TempData["EklemeSonuc"] = 1;
                blog.TAdd(bl);
                return RedirectToAction("Blogs");
            }

            return View();
        }
        public IActionResult BlogDetail(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;
            ViewBag.userId = userId;


            var result = blog.TGet(id);
            return View(result);
        }

        public IActionResult GetBlog(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = blog.TGet(id);
            ViewBag.getBlog = x;

            var cat = category.TGet(x.CategoryId);
            ViewBag.getCategory = cat;

            List<SelectListItem> values = (from y in c.Category.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.CategoryName,
                                               Value = y.CategoryId.ToString(),
                                           }).ToList();
            ViewBag.v = values;


            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View(x);
        }
        [HttpPost]
        public IActionResult BlogEdit(BlogEkle bg) //view yok
        {
            //int userId = Convert.ToInt16(GetCookie("userId"));
            //var degerler = user.TGet(userId);
            //ViewBag.user = degerler;

            //var x = blog.TGet(bg.UserId);
            //Blog bl = new Blog();
            //bl.UserId = userId;
            //if (ModelState.IsValid)
            //{
            //    if (bg.ImageUrl != null)
            //    {
            //        var extension = Path.GetExtension(bg.ImageUrl.FileName);
            //        var newImageName = Guid.NewGuid() + extension;
            //        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImageName);
            //        var stream = new FileStream(location, FileMode.Create);
            //        bg.ImageUrl.CopyTo(stream);
            //        bl.ImageUrl = newImageName;

            //        x.Text = bg.Text;
            //        x.BlogTitle = bg.BlogTitle;
            //        x.Status = false;
            //        x.Date = bg.Date;
            //        x._Category = bg._Category;
            //        x.CategoryId = bg.CategoryId;

            //        blog.TUpdate(x);
            //        TempData["GüncellemeSonuc"] = 1;
            //        return RedirectToAction("Blogs");
            //    }
                return View();
            }
       
            public IActionResult BlogDelete(int id) // view yok
            {
                int userId = Convert.ToInt16(GetCookie("userId"));
                var degerler = user.TGet(userId);
                ViewBag.user = degerler;

                var x = blog.TGet(id);
                if (x == null)
                {
                    return NotFound();
                }
                blog.TDelete(x);
                return RedirectToAction("Blogs");
            }

            public IActionResult Profil(int id)
            {
                if (id == 0)
                {
                    return NotFound();

                }
                int userId = Convert.ToInt16(GetCookie("userId"));
                var degerler = user.TGet(userId);
                ViewBag.user = degerler;
                var roles = role.TGet(degerler.RoleId);
                ViewBag.role = roles;
                ViewBag.userId = userId;
                return View(degerler);
            }
            [HttpPost]
            public IActionResult ProfilEdit(UserModel us)
            {

                int userId = Convert.ToInt16(GetCookie("userId"));
                var degerler = user.TGet(userId);
                ViewBag.user = degerler;
                ViewBag.userId = userId;

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
            public async Task<IActionResult> LogOut()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Response.Cookies.Delete("userId");

                return RedirectToAction("Index", "Home");
            }

        }
    }
