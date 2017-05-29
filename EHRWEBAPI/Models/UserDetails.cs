using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHRWEBAPI.Models
{
    public class UserDetails
    {
        public int UserID { get; set; }
        public Nullable<int> PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDoctor { get; set; }
    }
}