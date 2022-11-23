using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region AboutUs

        public IActionResult AboutUs()
        {
            IEnumerable<AboutUs> aboutus = _unitOfWork.AboutUs.GetAll();
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
                _unitOfWork.AboutUs.Add(ct);
                _unitOfWork.Save();
                return RedirectToAction("AboutUs", "Admin");
            }
            return View(ct);
        }
        public IActionResult AboutUsEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var AboutUsFromDbFirst = _unitOfWork.AboutUs.GetFirstOrDefault(u => u.Id == id);
            if (AboutUsFromDbFirst == null) { return NotFound(); }
            return View(AboutUsFromDbFirst);
        }
        [HttpPost]
        public IActionResult AboutUsEdit(AboutUs ct)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AboutUs.Update(ct);
                _unitOfWork.Save();
                return RedirectToAction("AboutUs");
            }
            return View(ct);
        }
        public IActionResult AboutUsDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var AboutUsFromDbFirst = _unitOfWork.AboutUs.GetFirstOrDefault(u => u.Id == id);
            if (AboutUsFromDbFirst == null) { return NotFound(); }
            return View(AboutUsFromDbFirst);
        }

        [HttpPost]
        public IActionResult AboutUsDeletePOST(int? id)
        {
            var obj = _unitOfWork.AboutUs.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.AboutUs.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("AboutUs");
        }
        #endregion

        #region Category

        //public IActionResult Category()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult Category()
        {
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();
            TempData["CategoryCount"] = categories.Count();
            return View(categories);
        }
        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryCreate(Category ct)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(ct);
                _unitOfWork.Save();
                return RedirectToAction("Category", "Admin");
            }
            return View(ct);
        }

        public IActionResult CategoryEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (CategoryFromDbFirst == null) { return NotFound(); }
            return View(CategoryFromDbFirst);
        }
        [HttpPost]
        public IActionResult CategoryEdit(Category ct)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(ct);
                _unitOfWork.Save();
                return RedirectToAction("Category");
            }
            return View(ct);
        }
        public IActionResult CategoryDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (CategoryFromDbFirst == null) { return NotFound(); }
            return View(CategoryFromDbFirst);
        }

        [HttpPost]
        public IActionResult CategoryDeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Category");
        }




        #endregion

        public IActionResult Contact()
        {
            IEnumerable<Contact> contact = _unitOfWork.Contact.GetAll();
            TempData["ContactCount"] = contact.Count();
            return View(contact);
        }




        #region Aboneler

        public IActionResult Subscriber()
        {
            IEnumerable<Subscriber> subs = _unitOfWork.Subscriber.GetAll();
            TempData["SubscriberCount"] = subs.Count();
            return View(subs);
        }


        #endregion
    }
}
