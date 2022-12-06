using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
