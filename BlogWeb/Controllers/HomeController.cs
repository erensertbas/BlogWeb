using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BlogDetail()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            IEnumerable<AboutUs> aboutus = _unitOfWork.AboutUs.GetAll();
            TempData["AboutUsCount"] = aboutus.Count();
            return View(aboutus);
        }

        public PartialViewResult getCategory()
        {
          
            return PartialView();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}