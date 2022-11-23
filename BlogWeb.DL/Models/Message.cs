using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        
    }
}
