using BlogWeb.BL.Repository;
using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.Controllers
{
    public class AdminController : Controller
    {
        BlogRepository blog = new BlogRepository();
        AboutUsRepository aboutUs = new AboutUsRepository();
        ContactRepository contact = new ContactRepository();
        public IActionResult Index()
        {
            return View();
        }
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
        public IActionResult AboutUsEdit(int id)
        {
            var x = aboutUs.TGet(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public IActionResult AboutUsEdit(AboutUs ct)
        {
            var x = aboutUs.TGet(ct.Id);
            if (ModelState.IsValid)
            {
                aboutUs.TUpdate(ct);
                return RedirectToAction("AboutUs");
            }
            return View(ct);
        }
        public IActionResult AboutUsDelete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var AboutUsFromDbFirst = aboutUs.TGet(id);
            if (AboutUsFromDbFirst == null) { return NotFound(); }
            return View(AboutUsFromDbFirst);
        }

        [HttpPost]
        public IActionResult AboutUsDeletePOST(int id)
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
        public IActionResult ContactEdit(int id)
        {
            var x = contact.TGet(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public IActionResult ContactEdit(Contact ct)
        {
            var x = contact.TGet(ct.Id);
            if (ModelState.IsValid)
            {
                contact.TUpdate(ct);
                return RedirectToAction("Contact");
            }
            return View(ct);
        }
        public IActionResult ContactDelete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ContactFromDbFirst = contact.TGet(id);
            if (ContactFromDbFirst == null) { return NotFound(); }
            return View(ContactFromDbFirst);
        }

        [HttpPost]
        public IActionResult ContactDeletePOST(int id)
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


    }
}
