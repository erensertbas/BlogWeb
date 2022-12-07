using BlogWeb.BL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.ViewComponents
{
    public class ContactGet: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ContactRepository contacts = new ContactRepository();
            var result = contacts.TList();
            return View(result);
        }
    }
}
