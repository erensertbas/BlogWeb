using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.Controllers
{
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
        public IActionResult AboutUs()
        {
            IEnumerable<AboutUs> aboutus = _unitOfWork.AboutUs.GetAll();
            return View(aboutus);
        }

    }
}
