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
        RoleRepository  role = new RoleRepository();    
        Context context = new Context();

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("_UserToken");
            var degerler = context.User.FirstOrDefault(x => x.UserId == userId);
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
            return View();
        }


        #region Profil 
        [HttpGet]
        public IActionResult Profil(int id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("_UserToken"));
            var degerler = user.TGet(userId);
            var roles = role.TGet(degerler.RoleId);
            ViewBag.role= roles;
            ViewBag.user = degerler;
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
                x.RoleId = 1;
                user.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return RedirectToAction("Profil",new {id});
              
            }
            return View();
        }


        #endregion

        #region Blog

        public IActionResult Blogs()
        {
            //IEnumerable<Blog> _blog = blog.TList();
            IEnumerable<Blog> _blog = context.Blog.Include(x => x._Category);
            TempData["BlogCount"] = _blog.Count();

            return View(_blog);
        }
        public IActionResult BlogDetail(int id)
        {
            var result = blog.TGet(id);
            return View(result);
        }

        public IActionResult BlogCreate()
        {
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
                bl.UserId = 1;
                bl.CategoryId = b.CategoryId;

                TempData["EklemeSonuc"] = 1;
                blog.TAdd(bl);
                return RedirectToAction("Blogs");
            }

            return View();
        }
        public IActionResult GetBlog(int id)
        {
            var x = blog.TGet(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View(x);
        }
        [HttpPost]
        public IActionResult BlogEdit(Blog bg) //view yok
        {
            var x = blog.TGet(bg.BlogId);
            //if (ModelState.IsValid)
            //{
            //    x.Text = ct.Text;
            //    aboutUs.TUpdate(x);
            //    return RedirectToAction("AboutUs");
            //}
            return View();
        }
        public IActionResult BlogDelete(int id) // view yok
        {
            var x = blog.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            blog.TDelete(x);
            return RedirectToAction("Blogs");
        }
        #endregion

        #region AboutUs

        public IActionResult AboutUs()
        {
            IEnumerable<AboutUs> aboutus = aboutUs.TList();
            TempData["AboutUsCount"] = aboutus.Count();
            return View(aboutus);
        }
        public IActionResult AboutUsCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AboutUsCreate(AboutUs ct)
        {
            if (ModelState.IsValid)
            {
                aboutUs.TAdd(ct);
                TempData["EklemeSonuc"] = 1;
                return RedirectToAction("AboutUs");
            }
            return View();
        }
        public IActionResult GetAbout(int id)
        {
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
            var x = aboutUs.TGet(ct.Id);
            if (ModelState.IsValid)
            {
                x.Text = ct.Text;
                aboutUs.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return RedirectToAction("AboutUs");
            }
            return View();
        }
        public IActionResult AboutUsDelete(int id)
        {
            var x = aboutUs.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            aboutUs.TDelete(x);
            return RedirectToAction("AboutUs");
        }
        #endregion


        #region Contact

        public IActionResult Contact()
        {
            IEnumerable<Contact> _contact = contact.TList();
            TempData["ContactCount"] = _contact.Count();
            return View(_contact);
        }
        public IActionResult ContactCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactCreate(Contact ct)
        {
            if (ModelState.IsValid)
            {
                contact.TAdd(ct);
                TempData["EklemeSonuc"] = 1;
                return RedirectToAction("Contact");
            }
            return View();
        }
        public IActionResult GetContact(int id)
        {
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
            var x = contact.TGet(ct.Id);
            if (ModelState.IsValid)
            {
                x.Address = ct.Address;
                x.EmailAddress = ct.EmailAddress;
                x.Phone = ct.Phone;
                contact.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return RedirectToAction("Contact");
            }
            return View();
        }
        public IActionResult ContactDelete(int id)
        {
            var x = contact.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            contact.TDelete(x);
            return RedirectToAction("Contact");
        }


        #endregion

        #region Category

        public IActionResult Category()
        {
            IEnumerable<Category> categories = category.TList();
            TempData["CategoryCount"] = categories.Count();

            return View(categories);
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryCreate(Category ca)
        {
            if (ModelState.IsValid)
            {
                category.TAdd(ca);
                TempData["EklemeSonuc"] = 1;
                return RedirectToAction("Category");
            }
            return View();
        }
        public IActionResult CategoryDelete(int id)
        {
            var x = category.TGet(id);
            if (x == null)
            {
                return NotFound();
            }
            category.TDelete(x);
            return RedirectToAction("Category");
        }

        public IActionResult GetCategory(int id)
        {
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
            var x = category.TGet(cat.CategoryId);
            if (ModelState.IsValid)
            {
                x.CategoryName = cat.CategoryName;
                x.CategoryId = cat.CategoryId;
                category.TUpdate(x);
                TempData["GüncellemeSonuc"] = 1;
                return RedirectToAction("Category");
            }
            return View();
        }

        #endregion



    }
}
