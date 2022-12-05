using BlogWeb.BL.Repository;
using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWeb.PL.Controllers
{
    public class AdminController : Controller
    {
        BlogRepository blog = new BlogRepository();
        AboutUsRepository aboutUs = new AboutUsRepository();
        ContactRepository contact = new ContactRepository();
        CategoryRepository category = new CategoryRepository();
        Context context = new Context();
        public IActionResult Index()
        {
            return View();
        }

        #region Blog

        public IActionResult Blogs()
        {
            IEnumerable<Blog> _blog = blog.TList();
            TempData["BlogCount"] = _blog.Count();
            return View(_blog);
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
        public IActionResult BlogCreate(Blog bg)
        {
            if (ModelState.IsValid)
            {
                blog.TAdd(bg);
                return RedirectToAction("Blogs");
            }
            /*
@Html.DropDownListFor(x=>x.CategoryId,(List<SelectListItem>) ViewBag.v,new { @class="form-control"})             
             */
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
                return RedirectToAction("Category");
            }
            return View();
        }

        #endregion



    }
}
