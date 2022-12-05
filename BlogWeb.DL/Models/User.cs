using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "İsim Boş Geçilemez!")]
        [Display(Name = "İsim")]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = "Soyisim Boş Geçilemez!")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "Mail Adresi Boş Geçilemez!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Boş Geçilemez!")]
        [Display(Name = "Şifre")]
        [MinLength(8), MaxLength(20)]
        public string Password { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role _Role { get; set; }
    }
}
