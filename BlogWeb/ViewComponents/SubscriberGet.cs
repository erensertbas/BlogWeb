using BlogWeb.BL.Repository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BlogWeb.PL.ViewComponents
{
    public class SubscriberGet:ViewComponent
    {
        SubscriberRepository subscriber = new SubscriberRepository();
        public IViewComponentResult Invoke()
        {

            //subscriber.TAdd(sub);
         
            return View();
        }
    }
}
