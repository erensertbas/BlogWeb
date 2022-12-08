﻿using BlogWeb.BL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.PL.ViewComponents
{
    public class NavGet : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            CategoryRepository category = new CategoryRepository();
            var result = category.TList();
            return View(result);
        }
    }
}
