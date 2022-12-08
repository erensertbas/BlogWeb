using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class BlogEkle
    {

        [Required(ErrorMessage = "Makale Başlık Boş Geçilemez!")]
        [Display(Name = "Makale Başlık ")]
        public string BlogTitle { get; set; }

        [Required(ErrorMessage = "Makale İçerik Boş Geçilemez!")]
        [Display(Name = "İçerik ")]
        public string Text { get; set; }
        public IFormFile ImageUrl { get; set; }

        [Required]
        public bool Status { get; set; }

        [Display(Name = "Tarih ")]
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        [Required(ErrorMessage = "Kategori Boş Geçilemez!")]
        public int CategoryId { get; set; }

    }
}
