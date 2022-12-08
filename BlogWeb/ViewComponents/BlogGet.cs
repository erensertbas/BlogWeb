using BlogWeb.BL.Repository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
namespace BlogWeb.PL.ViewComponents
{
    public class BlogGet : ViewComponent
    {
        BlogRepository blog = new BlogRepository();
        Context c=new Context();
        public IViewComponentResult Invoke()
        {
            //int page=1
            //BlogRepository blog = new BlogRepository();

            //return View(_blog);
            //IEnumerable<Blog> _blog = blog.TList();

            //var result = blog.TList().ToPagedList(page,4);

            var result = c.Blog.Distinct().OrderByDescending(d => d.Date);
            // güncelden eskiye doğru

            //var result = blog.TList();
            return View(result);
        }
    }
}
