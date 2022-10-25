using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
