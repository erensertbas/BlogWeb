using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "İsim Boş Geçilemez!")]

        [MinLength(3, ErrorMessage = "Ad 3-15 karakter uzunluğunda olmalı"), MaxLength(15)]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Soyisim Boş Geçilemez!")]

        [Display(Name = "Soyisim")]
        [MinLength(3, ErrorMessage = "Soyad 2-15 karakter uzunluğunda olmalı"), MaxLength(15)]

        public string Lastname { get; set; }
        [Required(ErrorMessage = "Mail Adresi Boş Geçilemez!")]

        [Display(Name = "Mail Adresi")]
        public string Email { get; set; }
        public Boolean Status { get; set; }
        [Required]
        [Display(Name = "Konu")]
        [MinLength(10, ErrorMessage = "Mesaj konusu 10-50 karakter uzunluğunda olmalı"), MaxLength(50)]
        public string Topic { get; set; }
        [MinLength(10, ErrorMessage = "Mesaj içeriği en az 10 karakter uzunluğunda olmalı")]
        [Display(Name = "Mesaj İçeriği")]
        [Required]
        public string Content { get; set; }
        
        
    }
}
