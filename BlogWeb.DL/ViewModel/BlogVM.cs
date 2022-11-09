using BlogWeb.DL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.ViewModel
{
    public class BlogVM
    {
        public Blog Blog { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    
    }
}
