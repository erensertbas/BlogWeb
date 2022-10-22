using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class Tag
    {
        [Key]
        public string TagId { get; set; }
        public string TagName { get; set; }

        public int BlogId { get; set; }
        [ForeignKey("BlogId")]
        //[ValidateNever]
        public Blog _Blog { get; set; }

    }
}
