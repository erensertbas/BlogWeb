using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class BlogUpdate
    {

        public int BlogId { get; set; }
        public string BlogTitle { get; set; }

        public string Text { get; set; }
        public string ImageUrl { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        
    }
}
