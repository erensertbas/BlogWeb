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
        public int Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }

    }
}
