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

        [Required(ErrorMessage = "Blog Başlık Boş Geçilemez!")]
        [Display(Name = "Blog Başlık ")]
        public string BlogTitle { get; set; }

        [Required(ErrorMessage = "Blog İçerik Boş Geçilemez!")]
        [Display(Name = "İçerik ")]
        public string Text { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public bool Status { get; set; }

        [Display(Name = "Tarih ")]
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        //  [ValidateNever]
        public UserModel _User { get; set; }

        [Required(ErrorMessage = "Kategori Boş Geçilemez!")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        //[ValidateNever]
        public Category _Category { get; set; }

    }
}
