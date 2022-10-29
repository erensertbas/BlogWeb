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
        public int Phone { get; set; }
        [Display(Name = "Mail Adres")]
        public string EmailAddress { get; set; }
        [Display(Name = "Adres")]
        public string Address { get; set; }

    }
}
