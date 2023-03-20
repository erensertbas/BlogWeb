using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error1(int code)
        {
            return View();
        }
    }
}
