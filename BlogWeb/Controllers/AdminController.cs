using BlogWeb.BL.Repository;
using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlogWeb.PL.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        BlogRepository blog = new BlogRepository();
        AboutUsRepository aboutUs = new AboutUsRepository();
        ContactRepository contact = new ContactRepository();
        CategoryRepository category = new CategoryRepository();
        UserRepository user = new UserRepository();
        SubscriberRepository subscriber = new SubscriberRepository();
        RoleRepository role = new RoleRepository();
        Context context = new Context();

        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

     

        }
      
        public string GetCookie(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string id);
            return id;
        }

        public IActionResult Index()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;


            IEnumerable<Blog> blogs = blog.TList();
            int Totalblog = blogs.Count();
            ViewBag.totalBlog = Totalblog;

            IEnumerable<Subscriber> subscribers = subscriber.TList();
            int TotalSubscriber = subscribers.Count();
            ViewBag.totatSubscriber = TotalSubscriber;

            IEnumerable<UserModel> users = user.TList();
            int TotalUser = users.Count();
            ViewBag.totalUser = TotalUser;


            var OnayBekleyen = blog.TList().Where(x => x.Status == false).Count();
            ViewBag.onayBekleyen = OnayBekleyen;



            return View();
        }
   
        #region Profil 
        [HttpGet]
     
        public IActionResult Profil(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;


            if (id == 0)
            {
                return NotFound();

            }
          
            var roles = role.TGet(degerler.RoleId);
            ViewBag.role = roles;
            ViewBag.user = degerler;
            return View(degerler);
        }
        [HttpPost]
        public IActionResult ProfilEdit(UserModel us)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;


            var x = user.TGet(us.UserId);
            int id = us.UserId;
            if (ModelState.IsValid)
            {
                x.FirstName = us.FirstName;
                x.LastName = us.LastName;
                x.Email = us.Email;
                x.Password = us.Password;
                x.RoleId = 1;
                user.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return RedirectToAction("Profil", new { id });

            }
            return View();
        }


        #endregion

        #region Blog

        public IActionResult AllBlogs()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;


            IEnumerable<Blog> _blog = context.Blog.Include(x => x._Category);
            TempData["BlogCount"] = _blog.Count();

            return View(_blog);
        }
        public IActionResult AllBlogDelete(int id) // view yok
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
            return Redirect("/Makaleler");
        }


        public IActionResult Blogs()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;


            IEnumerable<Blog> _blog = context.Blog.Include(x => x._Category).Where(x=>x.UserId==userId);
            TempData["BlogCount"] = _blog.Count();

            return View(_blog);
        }
       
        public IActionResult BlogDetail(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var result = blog.TGet(id);
            return View(result);
        }

        public IActionResult BlogCreate()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            List<SelectListItem> values = (from x in context.Category.ToList()
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

            Blog bl = new Blog();
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
                bl.Status = b.Status;
                bl.Date = b.Date;
                bl.UserId = userId;
                bl.CategoryId = b.CategoryId;

                TempData["EklemeSonuc"] = 1;
                blog.TAdd(bl);
                return Redirect("/Makalelerim");
            }

            return View();
        }
        public IActionResult GetBlog(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            List<SelectListItem> values = (from y in context.Category.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.CategoryName,
                                               Value = y.CategoryId.ToString(),
                                           }).ToList();
            ViewBag.v = values;

            var x = blog.TGet(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View(x);
        }
        [HttpPost]
        public IActionResult BlogEdit(BlogEkle b) //view yok
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            //Blog bl = new Blog();
            var blogKontrol = context.Blog.Where(x => x.UserId == userId).FirstOrDefault();

            //if (b.ImageUrl==null)
            //{
            //    b.ImageUrl = blogKontrol.ImageUrl;
            //}


            if (ModelState.IsValid)
            {
                if (b.ImageUrl != null)
                {
                    var extension = Path.GetExtension(b.ImageUrl.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    b.ImageUrl.CopyTo(stream);
                    blogKontrol.ImageUrl = newImageName;
                }

                blogKontrol.BlogTitle = b.BlogTitle;
                blogKontrol.Text = b.Text;
                blogKontrol.Status = b.Status;
                blogKontrol.Date = b.Date;
                blogKontrol.UserId = userId;
                blogKontrol.CategoryId = b.CategoryId;

                TempData["GuncellemeSonuc"] = 1;
                blog.TUpdate(blogKontrol);
                return Redirect("/Makalelerim");
            }

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
            return Redirect("/Makalelerim");
        }
        public IActionResult ApproveBlogList()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;
            var userBlog = blog.TList(x => x.Status == false);
            return View(userBlog);
        }


        public IActionResult ApproveBlog(int id)
        {

            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = blog.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            x.Status = true;
            blog.TUpdate(x);
            TempData["OnaylamaSonuc"] = 1;
            return Redirect("/MakaleOnayListesi");

        }
        #endregion

        #region AboutUs

        public IActionResult AboutUs()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            IEnumerable<AboutUs> aboutus = aboutUs.TList();
            TempData["AboutUsCount"] = aboutus.Count();
            return View(aboutus);
        }
        public IActionResult AboutUsCreate()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            return View();
        }
        [HttpPost]
        public IActionResult AboutUsCreate(AboutUs ct)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            if (ModelState.IsValid)
            {
                aboutUs.TAdd(ct);
                TempData["EklemeSonuc"] = 1;
                return Redirect("/YoneticiHakkimizda");

            }
            return View();
        }
        public IActionResult GetAbout(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = aboutUs.TGet(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View(x);
        }
        [HttpPost]
        public IActionResult AboutUsEdit(AboutUs ct)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = aboutUs.TGet(ct.Id);
            if (ModelState.IsValid)
            {
                x.Text = ct.Text;
                aboutUs.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return Redirect("/YoneticiHakkimizda");
            }
            return View();
        }
        public IActionResult AboutUsDelete(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = aboutUs.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            aboutUs.TDelete(x);
            return Redirect("/YoneticiHakkimizda");
        }
        #endregion


        #region Contact

        public IActionResult Contact()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            IEnumerable<Contact> _contact = contact.TList();
            TempData["ContactCount"] = _contact.Count();
            return View(_contact);
        }
        public IActionResult ContactCreate()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            return View();
        }
        [HttpPost]
        public IActionResult ContactCreate(Contact ct)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            if (ModelState.IsValid)
            {
                contact.TAdd(ct);
                TempData["EklemeSonuc"] = 1;
                return Redirect("/YoneticiIletisim");
            }
            return View();
        }
        public IActionResult GetContact(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = contact.TGet(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View(x);
        }
        [HttpPost]
        public IActionResult ContactEdit(Contact ct)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = contact.TGet(ct.Id);
            if (ModelState.IsValid)
            {
                x.Address = ct.Address;
                x.EmailAddress = ct.EmailAddress;
                x.Phone = ct.Phone;
                contact.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return Redirect("/YoneticiIletisim");
            }
            return View();
        }
        public IActionResult ContactDelete(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = contact.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            contact.TDelete(x);
            return Redirect("/YoneticiIletisim");
        }


        #endregion

        #region Category

        public IActionResult Category()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            IEnumerable<Category> categories = category.TList();
            TempData["CategoryCount"] = categories.Count();

            return View(categories);
        }

        public IActionResult CategoryCreate()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            return View();
        }
        [HttpPost]
        public IActionResult CategoryCreate(Category ca)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            if (ModelState.IsValid)
            {
                category.TAdd(ca);
                TempData["EklemeSonuc"] = 1;
                return Redirect("/YoneticiKategori");

            }
            return View();
        }
        public IActionResult CategoryDelete(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = category.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            category.TDelete(x);
            return Redirect("/YoneticiKategori");

            
        }

        public IActionResult GetCategory(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = category.TGet(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View(x);
        }

        [HttpPost]
        public IActionResult CategoryEdit(Category cat)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = category.TGet(cat.CategoryId);
            if (ModelState.IsValid)
            {
                x.CategoryName = cat.CategoryName;
                x.CategoryId = cat.CategoryId;
                category.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return Redirect("/YoneticiKategori");
            }
            return View();
        }

        #endregion


        #region Subsricer
        public IActionResult Subscriber()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            IEnumerable<Subscriber> result = subscriber.TList();
            TempData["SubscriberCount"]=result.Count();
            return View(result);
        }
        public IActionResult SubscriberCreate()
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            return View();
        }
        [HttpPost]
        public IActionResult SubscriberCreate(Subscriber sub)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            if (ModelState.IsValid)
            {
                subscriber.TAdd(sub);
                TempData["EklemeSonuc"] = 1;
                return Redirect("/AboneListesi");
            }
            return View();
        }

        //public IActionResult GetSubscriber(int id)
        //{
        //    int userId = Convert.ToInt16(GetCookie("userId"));
        //    var degerler = user.TGet(userId);
        //    ViewBag.user = degerler;

        //    var x = subscriber.TGet(id);
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    return View(x);
        //}
        //[HttpPost]
        //public IActionResult SubscriberEdit(Subscriber sub)
        //{
        //    int userId = Convert.ToInt16(GetCookie("userId"));
        //    var degerler = user.TGet(userId);
        //    ViewBag.user = degerler;

        //    var x = subscriber.TGet(sub.Id);
        //    if (ModelState.IsValid)
        //    {
        //        x.SubscriberMail = sub.SubscriberMail;
        //        subscriber.TUpdate(x);
        //        TempData["GüncellemeSonuc"] = 1;
        //        return Redirect("/AboneListesi");
        //    }
        //    return View();

        //}
        public IActionResult SubscriberDelete(int id)
        {
            int userId = Convert.ToInt16(GetCookie("userId"));
            var degerler = user.TGet(userId);
            ViewBag.user = degerler;

            var x = subscriber.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            subscriber.TDelete(x);
            return Redirect("/AboneListesi");
        }
        #endregion

    }
}
