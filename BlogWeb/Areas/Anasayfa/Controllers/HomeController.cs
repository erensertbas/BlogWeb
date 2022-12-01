using BlogWeb.BL.Repository.IRepository;
<<<<<<< HEAD:BlogWeb/Controllers/HomeController.cs
using BlogWeb.DL.Models;
=======
>>>>>>> master:BlogWeb/Areas/Anasayfa/Controllers/HomeController.cs
using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogWeb.PL.Areas.Anasayfa.Controllers
{
    [Area("Anasayfa")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
<<<<<<< HEAD:BlogWeb/Controllers/HomeController.cs
        public HomeController(IUnitOfWork unitOfWork)
=======
        public HomeController(ILogger<HomeController> logger)
>>>>>>> master:BlogWeb/Areas/Anasayfa/Controllers/HomeController.cs
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
              //Deneme
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