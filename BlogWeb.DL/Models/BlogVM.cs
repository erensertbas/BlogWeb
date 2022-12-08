using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    [DataContract]
    public class BlogVM
    {
        [DataMember]
        public Blog _blog { get; set; }
        [DataMember]
        public Category _category { get; set; }
        [DataMember]
        public User _user { get; set; }
    }
    
}
