using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class AboutUs
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Hakkımızda ")]
        public string Text { get; set; }
    }
}
