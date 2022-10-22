using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public string ImgUrl { get; set; }

        public int BlogId { get; set; }
        [ForeignKey("BlogId")]
        //[ValidateNever]
        public Blog _Blog { get; set; }
        //public int BlogId { get; set; }
        // resim hangi bloga ait ise blog id

    }
}
