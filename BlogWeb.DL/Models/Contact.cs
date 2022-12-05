using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Telefon")]
        [MaxLength(11)]
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Geçersiz Telefon Numarası!")]
        public string Phone { get; set; }
        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "Mail Adresi Boş Geçilemez!")]

        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Adres Boş Geçilemez!")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

    }
}
