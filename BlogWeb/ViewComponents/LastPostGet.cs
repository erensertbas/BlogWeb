using BlogWeb.BL.Repository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.ViewComponents
{
    public class LastPostGet : ViewComponent
    {
        BlogRepository blog = new BlogRepository();
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            //var result = c.Blog.Distinct().OrderByDescending(d => d.Date);
            var data = (from d in c.Blog
                        orderby d.BlogId descending
                        select d).Take(3);
            return View(data.ToList());
        }
    }
}
