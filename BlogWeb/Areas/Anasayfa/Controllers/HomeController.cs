using BlogWeb.BL.Repository.IRepository;
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
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}