using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class Subscriber
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mail Adresi Boş Geçilemez!")]
        public string SubscriberMail { get; set; }
    }
}
