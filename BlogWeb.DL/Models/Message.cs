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
        [Required]
        [Display(Name = "Ad")]
        [MinLength(3, ErrorMessage = "Ad 3-15 karakter uzunluğunda olmalı"), MaxLength(15)]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Soyad")]
        [MinLength(3, ErrorMessage = "Soyad 2-15 karakter uzunluğunda olmalı"), MaxLength(15)]

        public string Lastname { get; set; }
        [Required]
        [Display(Name = "Mail Adresi")]
        public string Email { get; set; }
        public Boolean Status { get; set; }
        [Required]
        [Display(Name = "Konu")]
        [MinLength(10, ErrorMessage = "Mesaj konusu 10-25 karakter uzunluğunda olmalı"), MaxLength(25)]
        public string Topic { get; set; }
        [MinLength(10, ErrorMessage = "Mesaj içeriği 10-100 karakter uzunluğunda olmalı"), MaxLength(100)]
        [Display(Name = "Mesaj İçeriği")]
        [Required]
        public string Content { get; set; }
        
    }
}
