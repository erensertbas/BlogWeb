using BlogWeb.BL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.ViewComponents
{
    public class AboutGet:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            AboutUsRepository aboutus = new AboutUsRepository();
            var result = aboutus.TList();
            return View(result);
        }
    }
}
