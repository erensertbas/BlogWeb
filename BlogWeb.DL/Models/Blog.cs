using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        public string BlogTitle { get; set; }

        [Required]
        public string BlogDescription { get; set; }

        public bool Status { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
      //  [ValidateNever]
        public User _User { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        //[ValidateNever]
        public Category _Category { get; set; }

    }
}
