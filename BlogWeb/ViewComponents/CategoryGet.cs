using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.ViewComponents
{
    public class CategoryGet:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public IViewComponentResult Invoke()
        {
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
            return View(categoryList);
        }
    }
}
