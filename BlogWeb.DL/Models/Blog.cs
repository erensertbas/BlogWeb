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
        [Display(Name = "Blog Başlık ")]

        public string BlogTitle { get; set; }

        [Required]
        [Display(Name = "İçerik ")]

        public string Text { get; set; }
        [Required]
        [Display(Name = "İçerik Alt ")]

        public string Text2 { get; set; }

        [Display(Name = "Durum ")]

        public bool Status { get; set; }
        [Display(Name = "Tarih ")]
        
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
