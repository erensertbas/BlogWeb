using BlogWeb.BL.Repository;
using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.ViewComponents
{
    public class AboutUsGet:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        //public IViewComponentResult Invoke()
        //{
        //    //AboutUsRepository aboutUsRepository;
        //    //var about = aboutUsRepository.GetAll();
        //    //return View(about);
        //}
    }
}
